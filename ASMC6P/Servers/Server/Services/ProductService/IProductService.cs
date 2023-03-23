using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductEntity>>> GetProductsAsync();
        Task<ServiceResponse<ProductEntity>> GetProductAsync(Guid productId);
        Task<ServiceResponse<List<ProductEntity>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<ServiceResponse<List<ProductEntity>>> GetFeaturedProducts();
        Task<ServiceResponse<List<ProductEntity>>> GetAdminProducts();
        Task<ServiceResponse<ProductEntity>> CreateProduct(ProductEntity product);
        Task<ServiceResponse<ProductEntity>> UpdateProduct(ProductEntity product);
        Task<ServiceResponse<bool>> DeleteProduct(Guid productId);

    }
}
