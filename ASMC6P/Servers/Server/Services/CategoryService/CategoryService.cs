using ASMC6P.Server.Repositories.CategoryRepositories;
using ASMC6P.Shared.Entities;

using Microsoft.EntityFrameworkCore;

namespace ASMC6P.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryEntity>> AddCategory(CategoryEntity category)
        {
            category.Editing = category.IsNew = false;
            _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return await GetAdminCategories();
        }

        public async Task<List<CategoryEntity>> DeleteCategory(Guid id)
        {
            CategoryEntity category = await GetCategoryById(id);
            if (category == null)
            {
                return new List<CategoryEntity>();
            }

            category.IsDeleted = true;
            await _categoryRepository.SaveChangesAsync();

            return await GetAdminCategories();
        }

        private async Task<CategoryEntity> GetCategoryById(Guid id)
        {
            return await _categoryRepository.AsQueryable().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CategoryEntity>> GetAdminCategories()
        {
            var categories = await _categoryRepository.AsQueryable()
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            return categories;
        }

        public async Task<List<CategoryEntity>> GetCategories()
        {
            var categories = await _categoryRepository.AsQueryable()
                .Where(c => !c.IsDeleted && c.Visible)
                .ToListAsync();
            return categories;
        }

        public async Task<List<CategoryEntity>> UpdateCategory(CategoryEntity category)
        {
            var dbCategory = await GetCategoryById(category.Id);
            if (dbCategory == null)
            {
                return null;
            }

            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.Visible = category.Visible;

            await _categoryRepository.SaveChangesAsync();

            return await GetAdminCategories();

        }
    }
}
