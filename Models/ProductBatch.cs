using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class ProductBatch
    {
        public int Id { get; set; }
        
        public DateTime ImportDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; } 
        [Column(TypeName = "int")]
        
        public int Quantity { get; set; } 
        [Column(TypeName = "int")]
        public int RemainingQuantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}