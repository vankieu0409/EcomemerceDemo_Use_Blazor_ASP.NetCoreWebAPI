using ASMC6P.Server.Data;
using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.CartRepositories;

public class CartRepository : RepositoryAsync<CartItemEntity>, ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context) : base(context, context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}