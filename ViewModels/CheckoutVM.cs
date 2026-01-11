
namespace doan_ttcn.ViewModels
{
    using doan_ttcn.Models;
    public class CheckoutVM
    {
        public bool IsUserLoggedIn { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string? Note { get; set; }

        public decimal SubTotal { get; set; }      
        public decimal GrandTotal { get; set; }   
        public decimal DiscountAmount { get; set; }
        public List<CartItemVM> CartItems { get; set; } = new List<CartItemVM>();
        public string PaymentMethod { get; set; }
    }

}