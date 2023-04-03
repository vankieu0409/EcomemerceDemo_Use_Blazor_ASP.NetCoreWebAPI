using ASMC6P.Server.Repositories.CartRepositories;
using ASMC6P.Server.Repositories.OrderRepositories;
using ASMC6P.Server.Services.CartService;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace ASMC6P.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Guid userId;


        public OrderService(IOrdersRepository ordersRepository, ICartService cartService, ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _ordersRepository = ordersRepository;
            _cartService = cartService;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderDetailsDto> GetOrderDetails(Guid orderId)
        {
            var response = new OrderDetailsDto();
            var order = await _ordersRepository.AsQueryable()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return response;
            }

            var orderDetailsResponse = new OrderDetailsDto
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = new List<OrderDetailsProductDto>()
            };

            order.OrderItems.ForEach(item =>
            orderDetailsResponse.Products.Add(new OrderDetailsProductDto
            {
                ProductId = item.ProductId,
                ImageUrl = item.Product.Image,
                Quantity = item.Quantity,
                Title = item.Product.Name,
                TotalPrice = item.TotalPrice
            }));

            response = orderDetailsResponse;

            return response;
        }

        public async Task<List<OrderOverviewDto>> GetOrders()
        {
            var response = new List<OrderOverviewDto>();
            var orders = await _ordersRepository.AsQueryable()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponse = new List<OrderOverviewDto>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1 ?
                    $"{o.OrderItems.First().Product.Name} and" +
                    $" {o.OrderItems.Count - 1} more..." :
                    o.OrderItems.First().Product.Name,
                ProductImageUrl = o.OrderItems.First().Product.Image
            }));

            response = orderResponse;

            return response;
        }

        public async Task<bool> PlaceOrder()
        {
            var products = (await _cartService.GetDbCartProducts());
            decimal totalPrice = 0;
            products.ForEach(product => totalPrice += product.Price * product.Quantity);

            var orderItems = new List<OrderItemEntity>();
            products.ForEach(product => orderItems.Add(new OrderItemEntity
            {
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                TotalPrice = product.Price * product.Quantity
            }));

            var order = new OrderEntity
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems
            };

            await _ordersRepository.AddAsync(order);

            var listCartItem = _cartRepository.AsQueryable().Where(ci => ci.UserId == userId).ToList();
            await _cartRepository.RemoveRangeAsync(listCartItem);

            await _cartRepository.SaveChangesAsync();

            return true;
        }
    }
}
