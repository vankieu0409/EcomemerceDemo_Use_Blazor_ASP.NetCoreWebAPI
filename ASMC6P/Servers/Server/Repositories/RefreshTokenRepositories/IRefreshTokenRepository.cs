using ASMC6P.Shared.Entities;
using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.RefreshTokenRepositories;

public interface IRefreshTokenRepository : IRepositoryAsync<RefreshTokenEntity>
{
}