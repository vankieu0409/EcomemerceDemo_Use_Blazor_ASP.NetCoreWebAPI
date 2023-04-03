using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using System.Net.Http.Json;

namespace ASMC6P.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public List<ProductEntity>? Products { get; set; } = new List<ProductEntity>();
        public string Message { get; set; } = "Loading products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;
        public List<ProductEntity> AdminProducts { get; set; }

        public event Action ProductsChanged;

        public async Task<ProductEntity> CreateProduct(ProductEntity product)
        {
            var result = await _http.PostAsJsonAsync("api/product", product);
            var newProduct = (await result.Content
                .ReadFromJsonAsync<ProductEntity>());
            return newProduct;
        }

        public async Task DeleteProduct(ProductEntity product)
        {
            var result = await _http.DeleteAsync($"api/product/{product.Id}");
        }

        public async Task GetAdminProducts()
        {

            var result = await _http
                .GetFromJsonAsync<List<ProductEntity>>($"api/Product/admin");
            AdminProducts = result;
            CurrentPage = 1;
            PageCount = 0;
            if (AdminProducts.Count == 0)
                Message = "No products found.";
        }

        public async Task<ProductEntity> GetProduct(Guid productId)
        {
            var result = await _http.GetFromJsonAsync<ProductEntity>($"api/Product/{productId}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<List<ProductEntity>>("/api/Product/featured") :
                await _http.GetFromJsonAsync<List<ProductEntity>>($"api/Product/category/{categoryUrl}");
            Products = result;
            if (result != null)
            {
                Products = result;
            }
            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
                Message = "No products found";

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<List<string>>($"api/Product/searchsuggestions/{searchText}");
            return result;
        }

        public async Task SearchProducts(string searchText)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ProductSearchResult>($"api/Product/search/{searchText}");
            if (result != null && result != null)
            {
                Products = result.Products;
                CurrentPage = result.CurrentPage;
                PageCount = result.Pages;
            }
            if (Products.Count == 0) Message = "No products found.";
            ProductsChanged?.Invoke();
        }

        public async Task<ProductEntity> UpdateProduct(ProductEntity product)
        {
            var result = await _http.PutAsJsonAsync($"api/Product", product);
            var content = await result.Content.ReadFromJsonAsync<ProductEntity>();
            return content;
        }
    }
}
