namespace doan_ttcn.ViewModels
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal SubTotal => Items.Sum(x => x.TotalPrice);
        public decimal DiscountAmount { get; set; } = 0;
        public decimal DiscountPercent { get; set; } = 0;

        // 3. Phí vận chuyển (Ví dụ cố định $3.00 hoặc 30.000đ như giao diện)
       
        // 4. Tổng thanh toán cuối cùng = (Tổng hàng - Giảm giá + Ship)
        public decimal GrandTotal => SubTotal - DiscountAmount ;

        // 5. Các thông tin về Voucher để hiển thị ra View
        public string AppliedCouponCode { get; set; } = ""; // Mã đang áp dụng
        public string CouponMessage { get; set; } = "";     // Thông báo lỗi/thành công
        public bool IsValid { get; set; } = false;
    }
}
