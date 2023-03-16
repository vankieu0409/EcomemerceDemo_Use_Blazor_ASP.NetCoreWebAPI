using ASMC6P.Shared.ViewModels;

namespace ASMC6P.Client.Services.Authentications;

public interface IAuthenticationService
{
    public Task<bool> LoginService(LoginUserViewModel viewModel);
    public Task<bool> RegiterService(CreateUserViewModel viewModel);
    public Task<HttpResponseMessage> RefreshToken();
    public Task<bool> LogoutService();
}