using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Server.Services.CartService
{
    public interface ICartService
    {
        Task<List<CartProductDto>> GetCartProducts(List<CartItemEntity> cartItems);
        Task<List<CartProductDto>> StoreCartItems(List<CartItemEntity> cartItems);
        Task<int> GetCartItemsCount();
        Task<List<CartProductDto>> GetDbCartProducts();
        Task<bool> AddToCart(CartItemEntity cartItem);
        Task<bool> UpdateQuantity(CartItemEntity cartItem);
        Task<bool> RemoveItemFromCart(Guid productId, Guid productTypeId);
    }
}
