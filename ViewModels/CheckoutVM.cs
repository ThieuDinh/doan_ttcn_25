using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.ViewModels
{
    public class CheckoutVM
    {
        // 1. Thông tin người mua hàng (Nhập từ form)
        public bool IsUserLoggedIn { get; set; }
        
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string? Note { get; set; }

        public decimal SubTotal { get; set; }      // Tổng tiền hàng
        public decimal GrandTotal { get; set; }    // Tổng thanh toán cuối cùng
        public decimal DiscountAmount { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string PaymentMethod { get; set; }
       
    }
    public class OrderSuccessVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}