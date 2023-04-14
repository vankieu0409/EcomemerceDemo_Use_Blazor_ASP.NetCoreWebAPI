using ASMC6P.Shared.Entities;

namespace ASMC6P.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<ProductEntity> Products { get; set; }
        List<ProductEntity> AdminProducts { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task<List<ProductEntity>> GetProducts(string? categoryUrl = null);
        Task<ProductEntity> GetProduct(Guid productId);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task<List<ProductEntity>> GetAdminProducts();
        Task<ProductEntity> CreateProduct(ProductEntity product);
        Task<ProductEntity> UpdateProduct(ProductEntity product);
        Task DeleteProduct(Guid product);
    }
}
