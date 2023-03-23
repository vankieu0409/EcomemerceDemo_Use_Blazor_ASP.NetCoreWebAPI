using ASMC6P.Server.Repositories.ProductTypeRepositories;
using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace ASMC6P.Server.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ServiceResponse<List<ProductTypeEntity>>> AddProductType(ProductTypeEntity productType)
        {
            productType.Editing = productType.IsNew = false;
            await _productTypeRepository.AddAsync(productType);
            await _productTypeRepository.SaveChangesAsync();

            return await GetProductTypes();
        }

        public async Task<ServiceResponse<List<ProductTypeEntity>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepository.AsQueryable().ToListAsync();
            return new ServiceResponse<List<ProductTypeEntity>> { Data = productTypes };
        }

        public async Task<ServiceResponse<List<ProductTypeEntity>>> UpdateProductType(ProductTypeEntity productType)
        {
            var dbProductType = await _productTypeRepository.AsQueryable().FirstOrDefaultAsync(c => c.Id == productType.Id);
            if (dbProductType == null)
            {
                return new ServiceResponse<List<ProductTypeEntity>>
                {
                    Success = false,
                    Message = "Product Type not found."
                };
            }

            dbProductType.Name = productType.Name;
            await _productTypeRepository.SaveChangesAsync();

            return await GetProductTypes();
        }
    }
}
