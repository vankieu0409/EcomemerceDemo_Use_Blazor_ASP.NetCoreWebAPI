using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.Entities;

namespace ASMC6P.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<CategoryEntity>> GetCategories();
        Task<List<CategoryEntity>> GetAdminCategories();
        Task<List<CategoryEntity>> AddCategory(CategoryEntity category);
        Task<List<CategoryEntity>> UpdateCategory(CategoryEntity category);
        Task<List<CategoryEntity>> DeleteCategory(Guid id);
    }
}
