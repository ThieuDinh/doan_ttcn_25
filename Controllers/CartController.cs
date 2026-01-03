using doan_ttcn.Data;
using doan_ttcn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using doan_ttcn.Helpers;
namespace doan_ttcn.Controllers;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    const string coupon_key = "applied_coupon";
    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }
    const string cart_key = "mycart";

    public List<CartItem> CART => HttpContext.Session.Get<List<CartItem>>(cart_key) ?? new List<CartItem>();
    public IActionResult Index()
    {
        var cart = CART;

        // Khởi tạo ViewModel thay vì chỉ truyền List
        var viewModel = new CartViewModel
        {
            Items = cart
        };

        // 1. Kiểm tra xem trong Session có lưu mã Voucher không
        var savedCoupon = HttpContext.Session.GetString(coupon_key);
        if (!string.IsNullOrEmpty(savedCoupon))
        {
            // Nếu có, thực hiện tính toán lại (để số liệu luôn đúng khi thêm/bớt sp)
            ApplyVoucherLogic(viewModel, savedCoupon);
        }

        // 2. Lấy thông báo từ TempData (khi vừa submit form áp dụng/hủy)
        if (TempData["CouponMessage"] != null)
        {
            viewModel.CouponMessage = TempData["CouponMessage"].ToString();
            viewModel.IsValid = (bool)(TempData["CouponIsValid"] ?? false);
        }

        return View(viewModel);

    }
    [HttpPost]
    public IActionResult ApplyCoupon(string couponCode)
    {
        var cart = CART;
        var viewModel = new CartViewModel { Items = cart };

        if (string.IsNullOrEmpty(couponCode))
        {
            TempData["CouponMessage"] = "Vui lòng nhập mã giảm giá!";
            TempData["CouponIsValid"] = false;
            return RedirectToAction("Index");
        }

        // Gọi logic kiểm tra và tính toán
        bool isSuccess = ApplyVoucherLogic(viewModel, couponCode);

        if (isSuccess)
        {
            // Thành công: Lưu mã vào Session
            HttpContext.Session.SetString(coupon_key, couponCode);
            TempData["CouponMessage"] = "Áp dụng mã giảm giá thành công!";
            TempData["CouponIsValid"] = true;
        }
        else
        {
            // Thất bại: Xóa mã khỏi Session (nếu có mã cũ sai)
            HttpContext.Session.Remove(coupon_key);
            TempData["CouponMessage"] = viewModel.CouponMessage; // Lấy lý do lỗi cụ thể
            TempData["CouponIsValid"] = false;
        }

        return RedirectToAction("Index");
    }
    public IActionResult RemoveCoupon()
    {
        HttpContext.Session.Remove(coupon_key);
        TempData["CouponMessage"] = "Đã hủy mã giảm giá.";
        TempData["CouponIsValid"] = true;
        return RedirectToAction("Index");
    }
    private bool ApplyVoucherLogic(CartViewModel model, string code)
    {
        var today = DateTime.Now;

        // 1. Tìm voucher trong DB
        var voucher = _context.Vouchers.FirstOrDefault(v => v.Code == code);

        // 2. Kiểm tra tồn tại và Active
        if (voucher == null || !voucher.IsActive)
        {
            model.CouponMessage = "Mã giảm giá không tồn tại hoặc đã bị khóa.";
            return false;
        }

        // 3. Kiểm tra ngày hiệu lực
        if (today < voucher.StartDate || today > voucher.EndDate)
        {
            model.CouponMessage = "Mã giảm giá chưa bắt đầu hoặc đã hết hạn.";
            return false;
        }

        // 4. Kiểm tra số lượng
        if (voucher.Quantity <= 0)
        {
            model.CouponMessage = "Mã giảm giá đã hết lượt sử dụng.";
            return false;
        }

        // 5. Kiểm tra giá trị đơn hàng tối thiểu (MinimumPrice)
        if (model.SubTotal < voucher.MinimumPrice)
        {
            model.CouponMessage = $"Đơn hàng phải từ {voucher.MinimumPrice.ToString("#,##0")}đ mới được dùng mã này.";
            return false;
        }

        // 6. Tính toán mức giảm giá
        // Công thức: SubTotal * % / 100
        decimal discount = model.SubTotal * voucher.DiscountPecent / 100;

        // Kiểm tra mức giảm tối đa (DícountMax - theo tên biến trong Voucher.cs)
        if (discount > voucher.DícountMax)
        {
            discount = voucher.DícountMax;
        }

        // Cập nhật kết quả vào ViewModel
        model.DiscountAmount = discount;
        model.DiscountPercent = voucher.DiscountPecent;
        model.AppliedCouponCode = code;
        model.IsValid = true;

        return true;
    }
    public IActionResult addToCart(int Id, int quantity = 1)
    {
        var cart = CART;
        var item = cart.FirstOrDefault(x => x.ProductId == Id);
        if (item == null)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            item = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = quantity
            };
            cart.Add(item);
        }
        else
        {
            item.Quantity += quantity;
        }

        HttpContext.Session.Set(cart_key, cart);

        return RedirectToAction("Index");
    }
    public IActionResult Remove(int id)
    {
        var cart = CART;
        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item != null)
        {
            cart.Remove(item);
            HttpContext.Session.Set(cart_key, cart);
        }
        return RedirectToAction("Index");
    }

    // 2. Hàm cập nhật số lượng (dùng cho cả nút cộng và trừ)
    public IActionResult UpdateQuantity(int id, int quantity)
    {
        var cart = CART;
        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item != null)
        {
            item.Quantity = quantity;
            // Nếu số lượng giảm xuống 0 hoặc âm thì xóa luôn
            if (item.Quantity <= 0)
            {
                cart.Remove(item);
            }
            HttpContext.Session.Set(cart_key, cart);
        }
        return RedirectToAction("Index");
    }
}