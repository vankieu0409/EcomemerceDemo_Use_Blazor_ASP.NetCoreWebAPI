using ASMC6P.Client.Services.Authentications;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Blazored.LocalStorage;

using System.Net.Http.Json;

namespace ASMC6P.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthenticationService _authenticationService;
        public List<CartProductDto> SelectItems { get; set; }


        public CartService(ILocalStorageService localStorage, HttpClient http, IAuthenticationService authenticationService)
        {
            _localStorage = localStorage;
            _http = http;
            _authenticationService = authenticationService;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItemEntity cartItem)
        {
            if (await _authenticationService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/add", cartItem);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemEntity>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItemEntity>();
                }

                var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId);
                if (sameItem == null)
                {
                    cart.Add(cartItem);
                }
                else
                {
                    sameItem.Quantity += cartItem.Quantity;
                }

                await _localStorage.SetItemAsync("cart", cart);
            }
            await GetCartItemsCount();
        }

        public async Task GetCartItemsCount()
        {
            if (await _authenticationService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync<int>("api/Cart/count");
                var count = result;

                await _localStorage.SetItemAsync<int>("cartItemsCount", count);

            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemEntity>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);

            }
            OnChange.Invoke();
        }

        public async Task<List<CartProductDto>> GetCartProducts()
        {
            if (await _authenticationService.IsUserAuthenticated())
            {
                var response = await _http.GetFromJsonAsync<List<CartProductDto>>("api/cart");
                return response;
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartItemEntity>>("cart");
                if (cartItems == null)
                    return new List<CartProductDto>();
                var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);
                var cartProducts =
                    await response.Content.ReadFromJsonAsync<List<CartProductDto>>();
                return cartProducts;
            }

        }

        public async Task RemoveProductFromCart(Guid productId)
        {
            if (await _authenticationService.IsUserAuthenticated())
            {
                await _http.DeleteAsync($"api/cart/{productId}");
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemEntity>>("cart");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == productId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItemEntity>>("cart");
            if (localCart == null)
            {
                return;
            }

            await _http.PostAsJsonAsync("api/cart", localCart);

            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
            }
        }

        public async Task UpdateQuantity(CartProductDto product)
        {
            if (await _authenticationService.IsUserAuthenticated())
            {
                var request = new CartItemEntity()
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                };
                await _http.PutAsJsonAsync("api/cart/update-quantity", request);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemEntity>>("cart");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == product.ProductId);
                if (cartItem != null)
                {
                    cartItem.Quantity = product.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }
    }
}
