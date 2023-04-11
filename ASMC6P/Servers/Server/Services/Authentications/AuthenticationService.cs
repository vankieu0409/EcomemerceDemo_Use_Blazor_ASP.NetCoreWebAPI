using ASMC6P.Server.Data;
using ASMC6P.Server.Repositories.RefreshTokenRepositories;
using ASMC6P.Server.Repositories.RoleRepositories;
using ASMC6P.Server.Repositories.UserRepositories;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;
using ASMC6P.Shared.ViewModels;

using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ASMC6P.Server.Services.Authentications;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public AuthenticationService(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager,
        ApplicationDbContext context, IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IMapper mapper,
        IHttpContextAccessor httpContextAccessor, RoleManager<RoleEntity> roleManager)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    public async Task<UserDto> Login(LoginUserViewModel viewModel)
    {
        var user = await _userManager.FindByNameAsync(viewModel.UserName);

        if (user == null) return new UserDto { Message = "User not found." };
        if (!await _userManager.CheckPasswordAsync(user, viewModel.Password))
            return new UserDto { Message = "Wrong Password." };
        var signManager = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, lockoutOnFailure: false);
        if (signManager.Succeeded)
        {
            var userResult = _mapper.Map<UserDto>(user);
            var token = CreateToken(user);
            var refreshToken = CreateRefreshToken();
            SetRefreshToken(refreshToken, user);
            userResult.Success = true;
            userResult.AccessToken = token;
            userResult.RefreshToken = refreshToken.Token;
            userResult.RefreshTokenExpireTime = refreshToken.Expires;
            return userResult;
        }
        else if (signManager.IsLockedOut)
        {
            return new UserDto { Message = "LockOut" };
        }
        else
        {
            return new UserDto { Message = " Invalid login attempt" };
        }

    }

    public async Task<UserDto> RegisterUser(CreateUserViewModel request)
    {
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            NormalizedUserName = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = request.Email,
            EmailConfirmed = true,
            NormalizedEmail = request.Email,
            PhoneNumber = "",
            PhoneNumberConfirmed = false,
            LockoutEnabled = false,
            LockoutEnd = DateTimeOffset.MinValue,
            AccessFailedCount = 0,
            IsDeleted = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false
        };
        await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, "Customer");


        await _context.SaveChangesAsync();
        var userDto = new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,

        };

        return userDto;
    }

    public async Task<UserDto> RefreshToken()
    {
        var a = _httpContextAccessor?.HttpContext?.Connection.LocalPort;
        var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
        var refreshTokenEntity = _refreshTokenRepository.AsQueryable().FirstOrDefault(c => c.RefreshToken == refreshToken && !c.IsUsed);
        var user = _userRepository.AsQueryable().FirstOrDefault(u => u.Id == refreshTokenEntity.UserId);
        if (refreshTokenEntity == null)
            return new UserDto() { Message = "Invalid Refresh Token" };
        else if (refreshTokenEntity.Expires < DateTime.Now)
            return new UserDto() { Message = "Token expired." };

        var token = CreateToken(user);
        var newRefreshToken = CreateRefreshToken();
        SetRefreshToken(newRefreshToken, user);

        refreshTokenEntity.IsUsed = true;
        await _refreshTokenRepository.UpdateAsync(refreshTokenEntity);
        await _refreshTokenRepository.SaveChangesAsync();

        return new UserDto()
        {
            Id = user.Id,
            Image = user.Image,
            Descreption = user.DisplayName,
            DisplayName = user.Decriptions,
            UserName = user.UserName,
            Success = true,
            AccessToken = token,
            RefreshToken = newRefreshToken.Token,
            RefreshTokenExpireTime = newRefreshToken.Expires
        };

    }

    public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
    {
        byte[] test;
        test = Convert.FromBase64String(passwordSalt);
        var hmac = new HMACSHA512(test);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(Encoding.UTF8.GetBytes(passwordHash));
    }

    public string CreatePasswordSalt(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public string CreateToken(UserEntity user)
    {
        var userRoles = _roleRepository.AsQueryable().FirstOrDefault(p =>
            p.Id == _context.UserRoles.Where(c => c.UserId == user.Id).Select(c => c.RoleId).FirstOrDefault());
        List<Claim> authClaims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, userRoles.NormalizedName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Jwt:ValidIssuer"],
            audience: _configuration["Authentication:Jwt:ValidAudience"],
            claims: authClaims,
            expires: DateTime.Now.AddHours(Convert.ToInt16(_configuration["Authentication:Jwt:ExpiresTime"])),
            signingCredentials: creds);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public RefreshTokenDto CreateRefreshToken()
    {
        var refreshToken = new RefreshTokenDto
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddHours(Convert.ToInt16(_configuration["Authentication:Jwt:ExpiresTime"])),
            Created = DateTime.Now
        };
        Console.WriteLine(JsonSerializer.Serialize(refreshToken));
        return refreshToken;
    }

    public void SetRefreshToken(RefreshTokenDto refreshToken, UserEntity user)
    {
        var refreshTokenEntity = new RefreshTokenEntity()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            IsUsed = false,
            RefreshToken = refreshToken.Token,
            Created = refreshToken.Created,
            Expires = refreshToken.Expires
        };
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.Expires,
            Secure = true
        };
        _httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        Console.WriteLine(refreshToken);

        _refreshTokenRepository.AddAsync(refreshTokenEntity);
        _refreshTokenRepository.SaveChangesAsync();
    }

    public bool Logout()
    {
        var logout = _signInManager.SignOutAsync();
        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("refreshToken");
        var a = logout.IsCompletedSuccessfully;
        if (logout.IsCompletedSuccessfully) return logout.IsCompletedSuccessfully;
        else if (logout.IsCanceled) return false;
        else if (logout.IsCompleted) return logout.IsCompleted;
        else return false;
    }

    public void UpdateRoles(RoleEntity role)
    {
        _roleManager.UpdateAsync(role);
    }

    public async Task<IList<string>> GetRolesOfUser(UserEntity user)
    {
        return await _userManager.GetRolesAsync(user);
    }
    public async Task<List<RoleEntity>> GetRoles()
    {
        var roleCollection = _roleManager.Roles.ToList();
        return roleCollection;
    }
}