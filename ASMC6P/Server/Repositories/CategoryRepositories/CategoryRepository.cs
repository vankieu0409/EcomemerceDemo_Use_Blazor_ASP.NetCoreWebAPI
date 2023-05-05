using ASMC6P.Server.Data;
using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.CategoryRepositories;

public class CategoryRepository : RepositoryAsync<CategoryEntity>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context, context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}