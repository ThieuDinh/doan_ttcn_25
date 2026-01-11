using doan_ttcn.Models;

namespace doan_ttcn.ViewModels
{
    public class AdminCreateOrderVM
    {
      
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }

        
        public List<int> ProductIds { get; set; } = new List<int>();
        public List<int> Quantities { get; set; } = new List<int>();
        
        public PaymentType PaymentMethod { get; set; } = PaymentType.COD;
        public OrderStatus Status { get; set; } = OrderStatus.Completed; 
    }
}