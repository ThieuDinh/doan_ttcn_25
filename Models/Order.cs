using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity; // Để dùng IdentityUser

namespace doan_ttcn.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Thông tin người nhận
        public string ShipToName { get; set; }
        public string ShipToAddress { get; set; }
        public string ShipToPhone { get; set; }
        public string? Note { get; set; }

        // Tài chính
        public decimal TotalAmount { get; set; } // Tổng tiền hàng
        public decimal DiscountAmount { get; set; } // Giảm giá
        public decimal FinalAmount { get; set; } // Khách phải trả

        // Trạng thái & Kênh
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderChannel Channel { get; set; } = OrderChannel.Website;

        // Liên kết User (Khách hàng)
        public string? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public IdentityUser? Customer { get; set; }

        // Danh sách sản phẩm mua
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}