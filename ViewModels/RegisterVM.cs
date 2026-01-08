using System.ComponentModel.DataAnnotations;
using doan_ttcn.ViewModels;
namespace doan_ttcn.ViewModels
{
    public class RegisterVM : LoginVM
    {
        public string? FullName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}

