using System.Net.Http.Json;
using System.Text.Json;

using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;
using ASMC6P.Shared.ViewModels;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;

namespace ASMC6P.Client.Services.Authentications;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<AuthenticationService> _logger;
    public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider,
    ILocalStorageService localStorage, ILogger<AuthenticationService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _authStateProvider = authStateProvider ?? throw new ArgumentNullException(nameof(authStateProvider));
        _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UserDto> LoginService(LoginUserViewModel viewModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Authentication/login", viewModel);
        var user = await result.Content.ReadFromJsonAsync<UserDto>();
        _logger.LogDebug($"Event-{user.Id} - Start");
        Console.WriteLine(user.UserName);
        user.Success = result.IsSuccessStatusCode;
        if (result.IsSuccessStatusCode)
        {
            await _localStorage.SetItemAsync("bearer", user.AccessToken);
            await _localStorage.SetItemAsync("user", user);
            await _authStateProvider.GetAuthenticationStateAsync();
        }
        _logger.LogDebug($"Event-{user.Id} - End - {JsonSerializer.Serialize(user)}");
        return user;
    }

    public async Task<bool> RegiterService(CreateUserViewModel viewModel)
    {
        viewModel.Role = "CUSTOMER";
        var result = await _httpClient.PostAsJsonAsync("api/Authentication/register", viewModel);
        return await Task.FromResult(result.IsSuccessStatusCode);
    }
    public async Task<List<RoleEntity>> GetRoleCollection()
    {
        var result = await _httpClient.GetFromJsonAsync<List<RoleEntity>>("/roles");
        return result;
    }

    public async Task<bool> Update(UpdateProfileVM vm)
    {
        var result = await _httpClient.PutAsJsonAsync("api/Authentication/update", vm);
        return result.IsSuccessStatusCode;
    }
    public async Task<HttpResponseMessage> RefreshToken()
    {

        var result = await _httpClient.GetAsync("api/Authentication/refresh-token");

        var user = await result.Content.ReadFromJsonAsync<UserDto>();
        if (result.IsSuccessStatusCode)
        {
            await _localStorage.RemoveItemsAsync(new List<string>() { "bearer", "user" });

            await _localStorage.SetItemAsync("bearer", user.AccessToken);
            await _localStorage.SetItemAsync("user", user);

        }
        return result;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
    }

    public async Task<bool> LogoutService()
    {
        var results = await _httpClient.GetStringAsync("Logout");
        await _localStorage.ClearAsync();
        var checkTokenLogout = string.IsNullOrEmpty(await _localStorage.GetItemAsStringAsync("bearer"));
        //_authStateProvider.NotifyAuthenticationStateChangedForLogout();
        return await Task.FromResult(checkTokenLogout);
    }
}