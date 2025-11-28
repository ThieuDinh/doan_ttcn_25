namespace doan_ttcn.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Giá chốt tại thời điểm mua

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}