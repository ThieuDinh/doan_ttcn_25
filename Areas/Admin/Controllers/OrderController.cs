using doan_ttcn.Data;
using doan_ttcn.Models;
using doan_ttcn.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace doan_ttcn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator,Manager,Staff")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product) 
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, int status)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();
            if (order.Status == OrderStatus.Cancelled)
            {
                TempData["Error"] = "Đơn hàng này đã bị hủy trước đó. Không thể khôi phục hoặc thay đổi trạng thái!";
                return RedirectToAction("Details", new { id = id });
            }
            if ((OrderStatus)status == OrderStatus.Cancelled && order.Status != OrderStatus.Cancelled)
            {
                foreach (var item in order.OrderDetails)
                {
                    var batch = await _context.ProductBatches
                        .Where(b => b.ProductId == item.ProductId && b.ExpireDate > DateTime.Now)
                        .OrderByDescending(b => b.ExpireDate)
                        .FirstOrDefaultAsync();

                    if (batch != null)
                    {
                        batch.RemainingQuantity += item.Quantity;
                        _context.Update(batch);
                    }
                    else
                    {
                        var anyBatch = await _context.ProductBatches
                             .Where(b => b.ProductId == item.ProductId)
                             .OrderByDescending(b => b.ImportDate)
                             .FirstOrDefaultAsync();

                        if (anyBatch != null)
                        {
                            anyBatch.RemainingQuantity += item.Quantity;
                            _context.Update(anyBatch);
                        }
                    }
                }
            }

            order.Status = (OrderStatus)status;
            _context.Update(order);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Cập nhật trạng thái đơn hàng #{id} thành công!";
            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateOrderVM model)
        {
            if (model.ProductIds == null || model.ProductIds.Count == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn ít nhất một sản phẩm.");
                ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
                return View(model);
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                 
                    decimal totalAmount = 0;
                    var orderDetails = new List<OrderDetail>();

                   
                    for (int i = 0; i < model.ProductIds.Count; i++)
                    {
                        int productId = model.ProductIds[i];
                        int quantity = model.Quantities[i];

                        var product = await _context.Products.FindAsync(productId);
                        if (product != null)
                        {
                            
                            var batches = _context.ProductBatches
                                .Where(b => b.ProductId == productId && b.RemainingQuantity > 0)
                                .OrderBy(b => b.ExpireDate) 
                                .ToList();

                            int quantityNeeded = quantity;
                            int totalStock = batches.Sum(b => b.RemainingQuantity);

                            if (totalStock < quantityNeeded)
                            {
                               
                                await transaction.RollbackAsync();
                                ModelState.AddModelError("", $"Sản phẩm {product.Name} không đủ hàng (Còn: {totalStock}).");
                                ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
                                return View(model);
                            }

                            foreach (var batch in batches)
                            {
                                if (quantityNeeded <= 0) break;
                                int take = Math.Min(batch.RemainingQuantity, quantityNeeded);
                                batch.RemainingQuantity -= take;
                                quantityNeeded -= take;
                                _context.Update(batch);
                            }

                            totalAmount += product.Price * quantity;
                            orderDetails.Add(new OrderDetail
                            {
                                ProductId = productId,
                                Quantity = quantity,
                                Price = product.Price
                            });
                        }
                    }

                    var order = new Order
                    {
                        OrderDate = DateTime.Now,
                        ShipToName = string.IsNullOrEmpty(model.CustomerName) ? "Khách lẻ (Tại quầy)" : model.CustomerName,
                        ShipToPhone = model.CustomerPhone ?? "",
                        ShipToAddress = model.CustomerAddress ?? "Mua tại cửa hàng",

                        TotalAmount = totalAmount,
                        DiscountAmount = 0,
                        FinalAmount = totalAmount,

                        Status = model.Status,
                        Channel = OrderChannel.InStore,
                        PaymentMethod = model.PaymentMethod,

                        OrderDetails = orderDetails
                    };

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    TempData["Message"] = "Tạo đơn hàng thành công!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                    ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
                    return View(model);
                }
            }
        }
    }
}