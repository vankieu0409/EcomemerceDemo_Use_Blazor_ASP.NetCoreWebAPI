using ASMC6P.Shared.Dtos;

namespace ASMC6P.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder();
        Task<List<OrderOverviewDto>> GetOrders();
        Task<OrderDetailsDto> GetOrderDetails(Guid orderId);
    }
}
