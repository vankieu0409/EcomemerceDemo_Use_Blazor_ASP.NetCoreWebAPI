using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.ViewModels;

namespace ASMC6P.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(CreateOrderViewModel products);
        Task<List<OrderOverviewDto>> GetOrders();
        Task<OrderDto> GetOrderDetails(Guid orderId);
    }
}
