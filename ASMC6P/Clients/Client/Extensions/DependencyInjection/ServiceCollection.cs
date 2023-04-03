using ASMC6P.Client.Extensions.BackgoundServices;
using ASMC6P.Client.Infrastructure.Authentication;
using ASMC6P.Client.Services.Authentications;
using ASMC6P.Client.Services.CartService;
using ASMC6P.Client.Services.CategoryService;
using ASMC6P.Client.Services.OrderService;
using ASMC6P.Client.Services.ProductService;

using Microsoft.AspNetCore.Components.Authorization;

using System.Reflection;

namespace ASMC6P.Client.Extensions.DependencyInjection;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        var entryAssembly = Assembly.GetEntryAssembly();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IProductService, ProductService>();
        //services.AddSingleton<IJSRuntime>(provider => provider.GetRequiredService<IJSRuntime>());
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddHttpContextAccessor();
        services.AddHostedService<CallRefreshToken>();

        return services;
    }
}