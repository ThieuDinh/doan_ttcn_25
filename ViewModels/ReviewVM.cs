using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.ViewModels
{
    public class ReviewVM
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }

        // Thông tin người dùng nhập (Form)
        public int Rating { get; set; } = 5; // Mặc định 5 sao

        [Required(ErrorMessage = "Vui lòng chia sẻ cảm nhận của bạn")]
        public string Content { get; set; }
    }
}