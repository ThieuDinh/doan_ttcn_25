// File: Models/ViewModels/AccountViewModels.cs
using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress]
        public string Email { get; set; } // Dùng Email làm tên đăng nhập

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : LoginViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}