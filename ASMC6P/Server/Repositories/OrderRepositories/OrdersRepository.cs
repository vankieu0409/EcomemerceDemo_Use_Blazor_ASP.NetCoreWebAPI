using ASMC6P.Server.Data;
using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.OrderRepositories;

public class OrdersRepository : RepositoryAsync<OrderEntity>, IOrdersRepository
{
    private readonly ApplicationDbContext _context;

    public OrdersRepository(ApplicationDbContext context) : base(context, context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
}