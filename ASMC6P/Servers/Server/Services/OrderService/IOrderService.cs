using ASMC6P.Shared.Dtos;

namespace ASMC6P.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(List<CartProductDto> products);
        Task<List<OrderOverviewDto>> GetOrders();
        Task<OrderDto> GetOrderDetails(Guid orderId);
    }
}
