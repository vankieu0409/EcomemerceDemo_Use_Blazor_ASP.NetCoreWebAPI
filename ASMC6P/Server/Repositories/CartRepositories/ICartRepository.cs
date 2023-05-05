using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.CartRepositories;

public interface ICartRepository : IRepositoryAsync<CartItemEntity>
{
}