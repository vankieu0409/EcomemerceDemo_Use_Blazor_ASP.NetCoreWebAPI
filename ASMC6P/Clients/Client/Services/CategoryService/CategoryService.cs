using ASMC6P.Shared.Entities;

using System.Net.Http.Json;

namespace ASMC6P.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
        public List<CategoryEntity> AdminCategories { get; set; } = new List<CategoryEntity>();

        public event Action OnChange;

        public async Task AddCategory(CategoryEntity category)
        {
            var response = await _http.PostAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<List<CategoryEntity>>());
            await GetCategories();
            OnChange.Invoke();
        }

        public CategoryEntity CreateNewCategory()
        {
            var newCategory = new CategoryEntity() { IsNew = true, Editing = true };
            AdminCategories.Add(newCategory);
            OnChange.Invoke();
            return newCategory;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var response = await _http.DeleteAsync($"api/Category/admin/{categoryId}");
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<List<CategoryEntity>>());
            await GetCategories();
            OnChange.Invoke();
        }

        public async Task GetAdminCategories()
        {
            var response = await _http.GetFromJsonAsync<List<CategoryEntity>>("api/Category/admin");
            if (response != null)
                AdminCategories = response;
        }

        public async Task<List<CategoryEntity>> GetCategories()
        {
            var response = await _http.GetFromJsonAsync<List<CategoryEntity>>("api/Category");
            if (response != null)
                return response;

            return null;
        }

        public async Task UpdateCategory(CategoryEntity category)
        {
            var response = await _http.PutAsJsonAsync("api/Category/admin", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<List<CategoryEntity>>());
            await GetCategories();
            OnChange.Invoke();
        }
    }
}
