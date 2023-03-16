using ASMC6P.Shared.Dtos;
using ASMC6P.Shared.ViewModels;

namespace ASMC6P.Client.Services.Authentications;

public interface IAuthenticationService
{
    public Task<UserDto> LoginService(LoginUserViewModel viewModel);
    public Task<bool> RegiterService(CreateUserViewModel viewModel);
    public Task<HttpResponseMessage> RefreshToken();
    public Task<bool> LogoutService();
}