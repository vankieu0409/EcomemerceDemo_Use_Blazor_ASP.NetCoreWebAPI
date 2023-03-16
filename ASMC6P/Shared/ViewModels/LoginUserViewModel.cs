using System.ComponentModel.DataAnnotations;

namespace ASMC6P.Shared.ViewModels;

public class LoginUserViewModel
{
    [Required(ErrorMessage = "Email không được để trống!")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password không được để trống!")]
    [RegularExpression(@"^(?!.* )(?=.*\d)(?=.*[A-Z]).{6,20}$", ErrorMessage = "Password từ 6-20 ký tự, có ít nhất 1 chữ số và 1 ký tự viết hoa!")]
    public string Password { get; set; } = string.Empty;
}