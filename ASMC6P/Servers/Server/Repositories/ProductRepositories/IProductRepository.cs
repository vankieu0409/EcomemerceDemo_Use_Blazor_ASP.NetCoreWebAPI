using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.ProductRepositories;

public interface IProductRepository : IRepositoryAsync<ProductEntity>
{
}