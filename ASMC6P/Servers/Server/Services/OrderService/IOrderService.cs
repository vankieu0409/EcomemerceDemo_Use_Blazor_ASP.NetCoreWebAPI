using ASMC6P.Shared.Dtos;

namespace ASMC6P.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderOverviewDto>>> GetOrders();
        Task<ServiceResponse<OrderDetailsDto>> GetOrderDetails(Guid orderId);
    }
}
