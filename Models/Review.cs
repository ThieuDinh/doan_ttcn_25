using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // Số sao (1-5)

        [Required(ErrorMessage = "Vui lòng nhập nội dung đánh giá")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Liên kết với Đơn hàng
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        // Liên kết với Người dùng (để hiện tên người đánh giá)
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; } 
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    }
}