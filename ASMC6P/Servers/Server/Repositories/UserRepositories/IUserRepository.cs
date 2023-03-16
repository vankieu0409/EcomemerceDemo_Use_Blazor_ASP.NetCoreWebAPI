using ASMC6P.Shared.Entities;

using EF.Support.RepositoryAsync;

namespace ASMC6P.Server.Repositories.UserRepositories;

public interface IUserRepository : IRepositoryAsync<UserEntity>
{
}