using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductDto>>> GetCartProducts(List<CartItemEntity> cartItems);
        Task<ServiceResponse<List<CartProductDto>>> StoreCartItems(List<CartItemEntity> cartItems);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartProductDto>>> GetDbCartProducts();
        Task<ServiceResponse<bool>> AddToCart(CartItemEntity cartItem);
        Task<ServiceResponse<bool>> UpdateQuantity(CartItemEntity cartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCart(Guid productId, Guid productTypeId);
    }
}
