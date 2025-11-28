using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(7,0)")]
        public decimal Price { get; set; } // Giá chốt tại thời điểm mua

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}