using ASMC6P.Server.Data;
using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.ProductTypeRepositories;

public class ProductTypeRepository : RepositoryAsync<ProductTypeEntity>, IProductTypeRepository
{
    private readonly ApplicationDbContext _context;

    public ProductTypeRepository(ApplicationDbContext context) : base(context, context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}