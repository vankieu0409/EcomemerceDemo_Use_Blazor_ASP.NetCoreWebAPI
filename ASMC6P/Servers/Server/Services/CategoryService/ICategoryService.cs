using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryEntity>>> GetCategories();
        Task<ServiceResponse<List<CategoryEntity>>> GetAdminCategories();
        Task<ServiceResponse<List<CategoryEntity>>> AddCategory(CategoryEntity category);
        Task<ServiceResponse<List<CategoryEntity>>> UpdateCategory(CategoryEntity category);
        Task<ServiceResponse<List<CategoryEntity>>> DeleteCategory(Guid id);
    }
}
