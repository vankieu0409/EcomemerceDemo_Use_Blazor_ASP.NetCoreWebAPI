using ASMC6P.Server.Services.OrderService;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderOverviewDto>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrdersDetails(Guid orderId)
        {
            var result = await _orderService.GetOrderDetails(orderId);
            return Ok(result);
        }
        [HttpPost()]
        public async Task<ActionResult<OrderDetailsDto>> PlaceOrders([FromBody] CreateOrderViewModel products)
        {
            var result = await _orderService.PlaceOrder(products.ProductCollection);
            return Ok(result);
        }
    }
}
