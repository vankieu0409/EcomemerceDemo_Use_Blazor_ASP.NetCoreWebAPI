﻿using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;
using ASMC6P.Shared.ViewModels;

namespace ASMC6P.Server.Services.Authentications;

public interface IAuthenticationService
{
    public Task<UserDto> Login(LoginUserViewModel request);
    public Task<UserDto> RegisterUser(CreateUserViewModel request);
    public Task<bool> Update(UpdateProfileVM resquest);
    public Task<UserDto> RefreshToken();
    string CreateToken(UserEntity user);
    RefreshTokenDto CreateRefreshToken();
    public void SetRefreshToken(RefreshTokenDto refreshToken, UserEntity user);
    public bool Logout();
    public void UpdateRoles(RoleEntity role);
    public Task<IList<string>> GetRolesOfUser(UserEntity user);

    public Task<List<RoleEntity>> GetRoles();
}