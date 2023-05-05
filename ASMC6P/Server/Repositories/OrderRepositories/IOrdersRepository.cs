using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.OrderRepositories;

public interface IOrdersRepository : IRepositoryAsync<OrderEntity>
{
}