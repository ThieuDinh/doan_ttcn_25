using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class Cart
    {
        
        public int Id { get; set; }

        // Liên kết 1-1 với User (Mỗi người 1 giỏ)
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        // Một giỏ chứa danh sách các món hàng
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        // Hàm tiện ích: Tính tổng tiền của cả giỏ (Dùng để hiển thị nhanh)
        public decimal TotalPrice => CartItems.Sum(x => x.TotalAmount);
    }
}