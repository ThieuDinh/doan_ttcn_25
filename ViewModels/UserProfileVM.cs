using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.ViewModels
{
    public class UserProfileVM
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
}