using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Server.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        Task<ServiceResponse<List<ProductTypeEntity>>> GetProductTypes();
        Task<ServiceResponse<List<ProductTypeEntity>>> AddProductType(ProductTypeEntity productType);
        Task<ServiceResponse<List<ProductTypeEntity>>> UpdateProductType(ProductTypeEntity productType);


    }
}
