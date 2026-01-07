using System.Security.Claims;
using doan_ttcn.Data;
using doan_ttcn.Helpers;
using doan_ttcn.Models;
using doan_ttcn.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CartItem = doan_ttcn.ViewModels.CartItem;

namespace doan_ttcn.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        const string cart_key = "mycart";
        const string coupon_key = "applied_coupon";

        // GET: /Checkout
        public IActionResult Index()
        {
            // 1. Lấy giỏ hàng
            var cart = HttpContext.Session.Get<List<CartItem>>(cart_key) ?? new List<CartItem>();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart"); // Giỏ trống thì đá về giỏ

            // 2. Tính toán tiền (Copy logic từ CartController hoặc tách ra Service)
            // Tạm thời tính nhanh để hiển thị
            var subTotal = cart.Sum(p => p.TotalPrice);
            decimal shippingFee = 3; // Hoặc 30000 tùy đơn vị bạn dùng
            decimal discount = 0;

            // Nếu có mã giảm giá thì tính lại (Logic đơn giản hóa để hiển thị)
            var couponCode = HttpContext.Session.GetString(coupon_key);
            if (!string.IsNullOrEmpty(couponCode))
            {
                var voucher = _context.Vouchers.FirstOrDefault(v => v.Code == couponCode);
                if (voucher != null)
                {
                    discount = subTotal * voucher.DiscountPecent / 100;
                    if (discount > voucher.DícountMax) discount = voucher.DícountMax;
                }
            }

            // 3. Tạo ViewModel
            var model = new CheckoutVM
            {
                CartItems = cart,

                SubTotal = subTotal,
                GrandTotal = subTotal - discount,
                DiscountAmount = discount
            };

            // 4. Nếu đã đăng nhập, tự điền thông tin User vào Form
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    model.FullName = user.FullName; // Hoặc FullName nếu có
                    model.Phone = user.PhoneNumber;
                    model.Address = user.Address;
                    model.Phone = user.PhoneNumber;// Nếu User có trường Address
                    model.IsUserLoggedIn = true;

                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM model)
        {
            // 1. Lấy giỏ hàng
            var cart = HttpContext.Session.Get<List<CartItem>>(cart_key) ?? new List<CartItem>();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");

            // 2. Tính toán lại tiền (Backend calculation)
            var subTotal = cart.Sum(p => p.TotalPrice);
            decimal shippingFee = 30000; // Lưu ý: Đồng bộ phí ship với hàm Index
            decimal discount = 0;

            var couponCode = HttpContext.Session.GetString(coupon_key);
            Voucher voucher = null;
            if (!string.IsNullOrEmpty(couponCode))
            {
                voucher = _context.Vouchers.FirstOrDefault(v => v.Code == couponCode);
                if (voucher != null && voucher.IsActive && voucher.Quantity > 0)
                {
                    discount = subTotal * voucher.DiscountPecent / 100;
                    if (discount > voucher.DícountMax) discount = voucher.DícountMax;
                }
            }
            decimal grandTotal = subTotal - discount + shippingFee;

            // 3. Xử lý Lưu đơn hàng
            if (ModelState.IsValid)
            {
                // Nếu payment method là COD hoặc các phương thức khác
                if (model.PaymentMethod == "COD" || model.PaymentMethod == "Banking")
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            // --- A. TRỪ KHO (Logic Batch FEFO) ---
                            foreach (var item in cart)
                            {
                                var batches = _context.ProductBatches
                                    .Where(b => b.ProductId == item.ProductId && b.RemainingQuantity > 0)
                                    .OrderBy(b => b.ExpireDate)
                                    .ThenBy(b => b.ImportDate)
                                    .ToList();

                                int quantityNeeded = item.Quantity;
                                int totalStock = batches.Sum(b => b.RemainingQuantity);

                                if (totalStock < quantityNeeded)
                                {
                                    await transaction.RollbackAsync();
                                    ModelState.AddModelError("", $"Sản phẩm {item.ProductName} chỉ còn {totalStock}, không đủ hàng.");
                                    // Setup lại dữ liệu hiển thị lỗi
                                    model.CartItems = cart;
                                    model.SubTotal = subTotal;
                                    model.GrandTotal = grandTotal;
                                    model.DiscountAmount = discount;
                                    return View("Index", model);
                                }

                                foreach (var batch in batches)
                                {
                                    if (quantityNeeded <= 0) break;
                                    if (batch.RemainingQuantity >= quantityNeeded)
                                    {
                                        batch.RemainingQuantity -= quantityNeeded;
                                        quantityNeeded = 0;
                                    }
                                    else
                                    {
                                        quantityNeeded -= batch.RemainingQuantity;
                                        batch.RemainingQuantity = 0;
                                    }
                                    _context.Update(batch);
                                }
                            }

                            // --- B. TẠO ĐƠN HÀNG (ORDER) ---
                            // Đây là đoạn bạn bị thiếu trong code cũ
                            var order = new Order
                            {
                                OrderDate = DateTime.Now,
                                ShipToName = model.FullName,
                                ShipToAddress = model.Address,
                                ShipToPhone = model.Phone,
                                Note = model.Note,
                                Status = OrderStatus.Pending,
                                Channel = OrderChannel.Website,
                                PaymentMethod = model.PaymentMethod == "COD" ? PaymentType.COD : PaymentType.Banking,

                                TotalAmount = subTotal,
                                DiscountAmount = discount,
                                FinalAmount = grandTotal,

                                VoucherId = voucher?.Id,
                                CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                            };

                            _context.Orders.Add(order);
                            await _context.SaveChangesAsync(); // Lưu để lấy OrderId

                            // --- C. TẠO CHI TIẾT ĐƠN HÀNG (ORDER DETAIL) ---
                            // Đoạn này bạn cũng bị thiếu
                            foreach (var item in cart)
                            {
                                var orderDetail = new OrderDetail
                                {
                                    OrderId = order.Id,
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    Price = item.Price
                                };
                                _context.OrderDetails.Add(orderDetail);
                            }

                            // Trừ số lượng Voucher nếu có
                            if (voucher != null)
                            {
                                voucher.Quantity -= 1;
                                _context.Update(voucher);
                            }

                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync(); // Xác nhận thành công

                            // Xóa giỏ hàng và voucher khỏi session
                            HttpContext.Session.Remove(cart_key);
                            HttpContext.Session.Remove(coupon_key);

                            return RedirectToAction("Success", new { orderId = order.Id });
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                        }
                    }
                }
            }

            // Nếu code chạy xuống đây nghĩa là có lỗi Validate hoặc lỗi Exception
            model.CartItems = cart;
            model.SubTotal = subTotal;
            model.GrandTotal = grandTotal;
            model.DiscountAmount = discount;
            return View("Index", model);
        }

        // Action thông báo thành công
        public IActionResult Success(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            // Nếu không tìm thấy (hoặc user gõ bừa URL), đá về trang chủ
            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // 2. Map dữ liệu sang ViewModel
            var viewModel = new OrderSuccessVM
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                FullName = order.ShipToName,
                Phone = order.ShipToPhone,
                Address = order.ShipToAddress,
                PaymentMethod = order.PaymentMethod == PaymentType.COD ? "Thanh toán khi nhận hàng (COD)" : "Chuyển khoản",
                Amount = order.FinalAmount,
                Note = order.Note
            };

            return View(viewModel);
        }
    }
}