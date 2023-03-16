using ASMC6P.Client.Infrastructure.Authentication;
using ASMC6P.Client.Services.Authentications;

using Microsoft.AspNetCore.Components.Authorization;

using System.Reflection;

namespace ASMC6P.Client.Infrastructure.Extensions.DependencyInjection;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        var entryAssembly = Assembly.GetEntryAssembly();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        //services.AddSingleton<IJSRuntime>(provider => provider.GetRequiredService<IJSRuntime>());
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        return services;
    }
}