using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItemEntity cartItem);
        Task<List<CartProductDto>> GetCartProducts();
        Task RemoveProductFromCart(Guid productId);
        Task UpdateQuantity(CartProductDto product);
        Task StoreCartItems(bool emptyLocalCart);
        Task GetCartItemsCount();
        List<CartProductDto> SelectItems { get; set; }
    }
}
