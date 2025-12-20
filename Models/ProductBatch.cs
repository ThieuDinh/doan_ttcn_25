using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class ProductBatch
    {
        public int Id { get; set; }
        [Column(TypeName = "date")] 
        public DateTime ImportDate { get; set; } = DateTime.Now;
        [Column(TypeName = "date")] 
        public DateTime ExpireDate { get; set; } 
        [Column(TypeName = "int")]
        
        public int Quantity { get; set; } 
        [Column(TypeName = "int")]
        public int RemainingQuantity { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}