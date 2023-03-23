using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.ProductVariantRepositories;

public interface IProductVariantRepository : IRepositoryAsync<ProductVariantEntity>
{
}