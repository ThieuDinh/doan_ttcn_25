using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Số lượng phải ít nhất là 1")]
        public int Quantity { get; set; }

        // Thuộc về Giỏ hàng nào
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [NotMapped] 
        public decimal TotalAmount => Quantity * (Product?.Price ?? 0);
    }
}