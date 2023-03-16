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
    private async void HandleLogin()
    {
        _processing = true;
        // ValidateFormLogin();
        var result = await _authentication.LoginService(userLogin);

        if (result)
        {
            _processing = false;
            await _jsRuntime.InvokeVoidAsync("alert", "Đăng nhập thành công!");

            _navigationManager.NavigateTo("/");
        }

    }

    #endregion

    #region UserRegister

    private async void RegisterHandle()
    {
        var isAccessed = await _authentication.RegiterService(userRegister);
        await _jsRuntime.InvokeVoidAsync("alert", "Đăng ký thành công");
        if (isAccessed) _navigationManager.NavigateTo("/login");
    }
    void ButtonTestclick()
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