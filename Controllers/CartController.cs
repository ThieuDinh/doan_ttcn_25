using doan_ttcn.Data;
using Microsoft.AspNetCore.Mvc;
using doan_ttcn.Helpers;
using Microsoft.AspNetCore.Authorization;
using doan_ttcn.ViewModels;
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

    public List<CartItemVM> CART => HttpContext.Session.Get<List<CartItemVM>>(cart_key) ?? new List<CartItemVM>();
    public IActionResult Index()
    {
        var cart = CART;
        var viewModel = new CartVM
        {
            Items = cart
        };
        var savedCoupon = HttpContext.Session.GetString(coupon_key);
        if (!string.IsNullOrEmpty(savedCoupon))
        {
        
            ApplyVoucherLogic(viewModel, savedCoupon);
        }

       
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
        var viewModel = new CartVM { Items = cart };

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
    private bool ApplyVoucherLogic(CartVM model, string code)
    {
        var today = DateTime.Now;

        var voucher = _context.Vouchers.FirstOrDefault(v => v.Code == code);

        if (voucher == null || !voucher.IsActive)
        {
            model.CouponMessage = "Mã giảm giá không tồn tại hoặc đã bị khóa.";
            return false;
        }
     
        if (today < voucher.StartDate || today > voucher.EndDate)
        {
            model.CouponMessage = "Mã giảm giá chưa bắt đầu hoặc đã hết hạn.";
            return false;
        }

        if (voucher.Quantity <= 0)
        {
            model.CouponMessage = "Mã giảm giá đã hết lượt sử dụng.";
            return false;
        }

        if (model.SubTotal < voucher.MinimumPrice)
        {
            model.CouponMessage = $"Đơn hàng phải từ {voucher.MinimumPrice.ToString("#,##0")}đ mới được dùng mã này.";
            return false;
        }

        decimal discount = model.SubTotal * voucher.DiscountPecent / 100;

        if (discount > voucher.DícountMax)
        {
            discount = voucher.DícountMax;
        }

        model.DiscountAmount = discount;
        model.DiscountPercent = voucher.DiscountPecent;
        model.AppliedCouponCode = code;
        model.IsValid = true;

        return true;
    }
    [Authorize]
    public IActionResult addToCart(int Id, int quantity = 1)
    {
        var totalStock = _context.ProductBatches
        .Where(b => b.ProductId == Id && b.ExpireDate >= DateTime.Now) // Chỉ lấy lô chưa hết hạn
        .Sum(b => b.RemainingQuantity);
        var cart = CART;
        var item = cart.FirstOrDefault(x => x.ProductId == Id);
        int currentQuantityInCart = item != null ? item.Quantity : 0;
        int requestedQuantity = currentQuantityInCart + quantity;

        if (requestedQuantity > totalStock)
        {
            TempData["Error"] = "Số lượng sản phẩm trong kho không đủ!";
            return RedirectToAction("Index"); // Hoặc trả về trang chi tiết
        }
        if (item == null)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            item = new CartItemVM
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

   
    public IActionResult UpdateQuantity(int id, int quantity)
    {
        var cart = CART;
        var item = cart.FirstOrDefault(x => x.ProductId == id);

        if (item != null)
        {
            var totalStock = _context.ProductBatches
            .Where(b => b.ProductId == id && b.ExpireDate >= DateTime.Now)
            .Sum(b => b.RemainingQuantity);

            if (quantity > totalStock)
            {
                TempData["Error"] = $"Kho chỉ còn {totalStock} sản phẩm!";
            
            }
            if (quantity <= totalStock)
            {
                item.Quantity = quantity;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
                HttpContext.Session.Set(cart_key, cart);
            }
        }
        return RedirectToAction("Index");
    }
}