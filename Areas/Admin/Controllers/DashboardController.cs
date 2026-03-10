using doan_ttcn.Data;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator,Manager,Staff")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index()
        {
            // 1. Tính tổng doanh thu (Chỉ tính những đơn KHÔNG bị hủy)
            // Dùng (decimal?) để tránh lỗi nếu bảng Order trống
            decimal totalRevenue = _context.Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .Sum(o => (decimal?)o.FinalAmount) ?? 0;

            // 2. Đếm số lượng đơn hàng đang chờ duyệt
            int pendingCount = _context.Orders
                .Count(o => o.Status == OrderStatus.Pending);

            // 3. Gửi dữ liệu sang View qua ViewBag
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.PendingCount = pendingCount;

            return View();
        }

        // API 1: Thống kê theo Tháng (trong năm hiện tại hoặc năm được chọn)
        [HttpGet]
        public IActionResult GetRevenueByMonth(int year)
        {
            var data = _context.Orders
                .Where(o => o.OrderDate.Year == year && o.Status != OrderStatus.Cancelled)
                .GroupBy(o => o.OrderDate.Month)
                .Select(g => new { Label = "Tháng " + g.Key, Value = g.Sum(o => o.FinalAmount) })
                .OrderBy(x => x.Label.Length).ThenBy(x => x.Label) // Sắp xếp label
                .ToList();
            return Json(data);
        }

        // API 2: Thống kê theo Năm (5 năm gần nhất)
        [HttpGet]
        public IActionResult GetRevenueByYear()
        {
            var data = _context.Orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .GroupBy(o => o.OrderDate.Year)
                .Select(g => new { Label = "Năm " + g.Key, Value = g.Sum(o => o.FinalAmount) })
                .OrderBy(x => x.Label)
                .ToList();
            return Json(data);
        }

        // API 3: Top Sản phẩm bán chạy
        [HttpGet]
        public IActionResult GetTopProducts()
        {
            var data = _context.OrderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new
                {
                    Label = g.First().Product.Name,
                    Value = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.Value)
                .Take(5)
                .ToList();
            return Json(data);
        }
        public IActionResult GetRevenueByChannel()
{
    var data = _context.Orders
        .Where(o => o.Status != OrderStatus.Cancelled) // Chỉ tính đơn thành công
        .GroupBy(o => o.Channel)
        .Select(g => new { 
            Label = g.Key.ToString(), // Tên kênh (Website, InStore...)
            Value = g.Sum(o => o.FinalAmount) 
        })
        .ToList();
        
    return Json(data);
}
    }
}