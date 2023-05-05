using ASMC6P.Shared.Dtos;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;

namespace ASMC6P.Client.Extensions.BackgoundServices;

public class CallRefreshToken : BackgroundService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ILogger<CallRefreshToken> _logger;

    public CallRefreshToken(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, ILocalStorageService localStorage, ILogger<CallRefreshToken> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;
        _localStorage = localStorage;
        _logger = logger;
    }

    private DateTime GetExpOfAccessToken()
    {
        var refreshToken = _httpContextAccessor?.HttpContext.Request.Cookies["refreshToken"];
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(refreshToken);

        if (jwtToken != null && jwtToken.Payload != null)
        {
            if (jwtToken.Payload.Exp != null && jwtToken.Payload.Exp is long)
            {
                var a = DateTimeOffset.FromUnixTimeSeconds((long)jwtToken.Payload.Exp).UtcDateTime;
                return DateTimeOffset.FromUnixTimeSeconds((long)jwtToken.Payload.Exp).UtcDateTime;
            }
        }

        // Return a default value if the refresh token is not valid or does not contain an expiration time
        return DateTime.MinValue;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug($"CallRefreshToken - Start Handle");
        var expireTime = GetExpOfAccessToken().Minute;
        _logger.LogDebug($"CallRefreshToken - Handle {expireTime}");
        while (!stoppingToken.IsCancellationRequested)
        {
            // Gọi RefreshToken
            var result = await _httpClient.GetAsync("api/Authentication/refresh-token");

            var user = await result.Content.ReadFromJsonAsync<UserDto>();
            if (result.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemsAsync(new List<string>() { "bearer", "user" });

                await _localStorage.SetItemAsync("bearer", user.AccessToken);
                await _localStorage.SetItemAsync("user", user);
                _logger.LogDebug($"CallRefreshToken - End Handle {nameof(user)}  \n Payload: \n {JsonSerializer.Serialize(user)}");
            }

            // Ngủ 30 phút trước khi gọi RefreshToken tiếp theo
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

        }
    }
}