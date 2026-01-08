namespace doan_ttcn.ViewModels
{
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; } = new List<CartItemVM>();
        public decimal SubTotal => Items.Sum(x => x.TotalPrice);
        public decimal DiscountAmount { get; set; } = 0;
        public decimal DiscountPercent { get; set; } = 0;
        public decimal GrandTotal => SubTotal - DiscountAmount;

        public string AppliedCouponCode { get; set; } = "";
        public string CouponMessage { get; set; } = "";
        public bool IsValid { get; set; } = false;
    }
}