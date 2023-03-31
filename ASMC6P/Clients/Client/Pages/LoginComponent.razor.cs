using ASMC6P.Shared.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor;

namespace ASMC6P.Client.Pages;

[AllowAnonymous]
public partial class LoginComponent : ComponentBase
{

    public List<string> _messErorrCollection = new();
    [Inject] private IDialogService DialogService { get; set; }
    private LoginUserViewModel userLogin { get; set; } = new();

    private CreateUserViewModel userRegister { get; set; } = new();

    bool isShow;
    InputType PasswordInput = InputType.Password;
    string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    #region userLogin



    private async Task ShowAlert(string mess)
    {
        await _jsRuntime.InvokeVoidAsync("alert", mess);
    }
    private bool _processing = false;

    async Task ProcessSomething()
    {
        _processing = true;
        await Task.Delay(2000);
        _processing = false;
    }
    private async Task HandleLogin()
    {
        _processing = true;
        // ValidateFormLogin();
        var result = await _authentication.LoginService(userLogin);
        _processing = false;
        if (result.Success)
        {
            await DialogService.ShowMessageBox("Thông báo", "Đăng nhập Thành Công!", yesText: "Triển thôi!");
            _navigationManager.NavigateTo("/");
        }
        else
        {
            await DialogService.ShowMessageBox("Thông báo", result.Message, yesText: "Làm lại!");
            _navigationManager.NavigateTo("/login");
        }

    }

    #endregion

    #region UserRegister

    private async Task RegisterHandle()
    {
        userRegister.Role = "Customers";
        _processing = true;
        var isAccessed = await _authentication.RegiterService(userRegister);
        await DialogService.ShowMessageBox("Thông báo", "Đăng ký Thành Công!", yesText: "Triển thôi!");
        _processing = false;
        if (isAccessed) _navigationManager.NavigateTo("/login");
    }
    void ButtonTestClick()
    {
        if (isShow)
        {
            isShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }
    #endregion
}