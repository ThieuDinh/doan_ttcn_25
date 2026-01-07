using doan_ttcn.Models;

namespace doan_ttcn.ViewModels
{
    public class AdminCreateOrderVM
    {
        // Thông tin khách hàng (Có thể để trống nếu khách vãng lai)
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }

        // Mảng chứa ID sản phẩm và Số lượng được chọn từ giao diện
        public List<int> ProductIds { get; set; } = new List<int>();
        public List<int> Quantities { get; set; } = new List<int>();
        
        public PaymentType PaymentMethod { get; set; } = PaymentType.COD;
        public OrderStatus Status { get; set; } = OrderStatus.Completed; // Mặc định là Hoàn thành nếu mua tại quầy
    }
}