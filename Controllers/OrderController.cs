using System.Security.Claims;
using doan_ttcn.Data;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doan_ttcn.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Where(o => o.CustomerId == userId)
                .OrderByDescending(o => o.OrderDate) // Đơn mới nhất lên đầu
                .ToListAsync();

            return View(orders);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product) // Load kèm thông tin sản phẩm
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);

            if (order == null) return NotFound();

            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.CustomerId == userId);

            if (order == null) return NotFound();

            if (order.Status == OrderStatus.Pending) // Chỉ hủy được đơn mới đặt
            {
                var orderDetails = await _context.OrderDetails.Where(od => od.OrderId == id).ToListAsync();

                foreach (var item in orderDetails)
                {
                    // Tìm Batch phù hợp để trả hàng về (Ưu tiên Batch còn hạn xa nhất hoặc Batch nhập mới nhất)
                    var batch = await _context.ProductBatches
                        .Where(b => b.ProductId == item.ProductId && b.ExpireDate > DateTime.Now)
                        .OrderByDescending(b => b.ExpireDate) // Trả vào lô có hạn xa nhất
                        .FirstOrDefaultAsync();

                    if (batch != null)
                    {
                        batch.RemainingQuantity += item.Quantity;
                        _context.Update(batch);
                    }
                  
                }
                order.Status = OrderStatus.Cancelled;
                _context.Update(order);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã hủy đơn hàng thành công.";
            }
            else
            {
                TempData["Error"] = "Không thể hủy đơn hàng này (Đã giao hoặc đang vận chuyển).";
            }

            return RedirectToAction("Index");
        }
    }
}