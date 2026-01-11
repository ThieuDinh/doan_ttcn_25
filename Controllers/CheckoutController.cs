using System.Security.Claims;
using doan_ttcn.Data;
using doan_ttcn.Helpers;
using doan_ttcn.Models;
using doan_ttcn.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


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

    
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItemVM>>(cart_key) ?? new List<CartItemVM>();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart"); 

            var subTotal = cart.Sum(p => p.TotalPrice);
            decimal shippingFee = 0; 
            decimal discount = 0;

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

            var model = new CheckoutVM
            {
                CartItems = cart,

                SubTotal = subTotal,
                GrandTotal = subTotal - discount,
                DiscountAmount = discount
            };

         
         
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    model.FullName = user.FullName; 
                    model.Phone = user.PhoneNumber;
                    model.Address = user.Address;
                    model.Phone = user.PhoneNumber;
                    model.IsUserLoggedIn = true;

                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM model)
        {
            // 1. Lấy giỏ hàng
            var cart = HttpContext.Session.Get<List<CartItemVM>>(cart_key) ?? new List<CartItemVM>();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");

          
            var subTotal = cart.Sum(p => p.TotalPrice);
            decimal shippingFee = 0;
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

          
            if (ModelState.IsValid)
            {
              
                if (model.PaymentMethod == "COD" || model.PaymentMethod == "Banking")
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                           
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
                            await _context.SaveChangesAsync(); 

                
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

                         
                            if (voucher != null)
                            {
                                voucher.Quantity -= 1;
                                _context.Update(voucher);
                            }

                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync(); 

                          
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