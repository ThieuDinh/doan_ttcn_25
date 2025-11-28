namespace doan_ttcn.Models
{
    public class ProductBatch
    {
        public int Id { get; set; }
        
        public DateTime ImportDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; } 
        
        public int Quantity { get; set; } 
        public int RemainingQuantity { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}