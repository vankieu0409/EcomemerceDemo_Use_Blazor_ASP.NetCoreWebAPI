using System.ComponentModel.DataAnnotations;

namespace ASMC6P.Shared.ViewModels;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Không được để trống!")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Không được để trống!")]
    [RegularExpression(@"^(?!.* )(?=.*\d)(?=.*[A-Z]).{6,20}$",
        ErrorMessage = "Password từ 6-20 ký tự, có ít nhất 1 chữ số và 1 ký tự viết hoa!")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Không được để trống!")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
    public string Email { get; set; } = string.Empty;
}