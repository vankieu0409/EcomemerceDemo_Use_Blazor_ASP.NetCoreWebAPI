using ASMC6P.Server.Repositories.CartRepositories;
using ASMC6P.Server.Repositories.ProductRepositories;
using ASMC6P.Server.Repositories.ProductVariantRepositories;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace ASMC6P.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Guid userId;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IProductVariantRepository productVariantRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _productVariantRepository = productVariantRepository;
            _httpContextAccessor = httpContextAccessor;
            userId = Guid.Parse(_httpContextAccessor?.HttpContext?.Request.Cookies["userid"]);
        }

        public async Task<ServiceResponse<List<CartProductDto>>> GetCartProducts(List<CartItemEntity> cartItems)
        {
            var result = new ServiceResponse<List<CartProductDto>>
            {
                Data = new List<CartProductDto>()
            };

            foreach (var item in cartItems)
            {
                var product = await _productRepository.AsQueryable()
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var productVariant = await _productVariantRepository.AsQueryable()
                    .Where(v => v.ProductId == item.ProductId
                        && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant == null)
                {
                    continue;
                }

                var cartProduct = new CartProductDto
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductDto>>> StoreCartItems(List<CartItemEntity> cartItems)
        {
            var userId = Guid.Parse(_httpContextAccessor?.HttpContext?.Request.Cookies["userid"]);
            cartItems.ForEach(cartItem => cartItem.UserId = userId);
            await _cartRepository.AddRangeAsync(cartItems);
            await _cartRepository.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = _cartRepository.AsQueryable().Where(ci => ci.UserId == userId).ToList().Count;
            return Task.FromResult(new ServiceResponse<int> { Data = count });
        }

        public async Task<ServiceResponse<List<CartProductDto>>> GetDbCartProducts()
        {

            return await GetCartProducts(_cartRepository.AsQueryable().Where(ci => ci.UserId == userId).ToList());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItemEntity cartItem)
        {
            cartItem.UserId = userId;

            var sameItem = await _cartRepository.AsQueryable()
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
                ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == cartItem.UserId);
            if (sameItem == null)
            {
                await _cartRepository.AddAsync(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _cartRepository.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItemEntity cartItem)
        {
            var dbCartItem = await _cartRepository.AsQueryable()
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
                ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == userId);
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            dbCartItem.Quantity = cartItem.Quantity;
            await _cartRepository.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(Guid productId, Guid productTypeId)
        {
            var dbCartItem = await _cartRepository.AsQueryable()
                .FirstOrDefaultAsync(ci => ci.ProductId == productId &&
                ci.ProductTypeId == productTypeId && ci.UserId == userId);
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            await _cartRepository.AddAsync(dbCartItem);
            await _cartRepository.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
