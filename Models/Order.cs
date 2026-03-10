using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity; // Để dùng IdentityUser

namespace doan_ttcn.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string ShipToName { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string ShipToAddress { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string ShipToPhone { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Note { get; set; }

        // Tài chính
        [Column(TypeName = "decimal(9,0)")]
        public decimal TotalAmount { get; set; } 
        [Column(TypeName = "decimal(9,0)")]
        public decimal DiscountAmount { get; set; }
        [Column(TypeName = "decimal(9,0)")]        
        public decimal FinalAmount { get; set; } 

        [Column(TypeName = "int")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        [Column(TypeName = "int")]
        public OrderChannel Channel { get; set; } = OrderChannel.Website;

        public string? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser? Customer { get; set; }

       
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public int? VoucherId { get; set; } 

      
        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }
        public PaymentType PaymentMethod { get; set; } = PaymentType.COD;
        public bool IsReviewed { get; set; } = false;
    }
}