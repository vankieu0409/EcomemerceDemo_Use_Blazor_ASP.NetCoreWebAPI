using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Net.Http.Json;

namespace ASMC6P.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public OrderService(HttpClient http,
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<OrderDto> GetOrderDetails(Guid orderId)
        {
            var result = await _http.GetFromJsonAsync<OrderDto>($"api/order/{orderId}");
            return result;
        }

        public async Task<List<OrderOverviewDto>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<List<OrderOverviewDto>>("api/order");
            return result;
        }

        public async Task<bool> PlaceOrder(CreateOrderViewModel products)
        {

            var result = await _http.PostAsJsonAsync($"api/order", products);

            return result.IsSuccessStatusCode;

        }
        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
