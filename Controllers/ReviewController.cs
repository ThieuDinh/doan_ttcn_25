// Controllers/ReviewController.cs
using System.Security.Claims;
using doan_ttcn.Data;
using doan_ttcn.Models;
using doan_ttcn.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create(int orderId, int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    
        var orderDetail = _context.OrderDetails
            .Include(od => od.Product)
            .Include(od => od.Order)
            .FirstOrDefault(od => od.OrderId == orderId &&
                                  od.ProductId == productId &&
                                  od.Order.CustomerId == userId);

        if (orderDetail == null) return NotFound();

     
        if (orderDetail.Order.Status != OrderStatus.Completed)
        {
            TempData["Error"] = "Đơn hàng chưa hoàn thành.";
            return RedirectToAction("Detail", "Order", new { id = orderId });
        }

        if (orderDetail.IsReviewed)
        {
            TempData["Error"] = "Sản phẩm này đã được đánh giá.";
            return RedirectToAction("Detail", "Order", new { id = orderId });
        }

      
        var vm = new ReviewVM
        {
            OrderId = orderId,
            ProductId = productId,
            ProductName = orderDetail.Product.Name,
            ProductImage = orderDetail.Product.ImageUrl,
            Rating = 5
        };

        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> SubmitReview(ReviewVM model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    
        var orderDetail = _context.OrderDetails
            .Include(od => od.Order)
            .FirstOrDefault(od => od.OrderId == model.OrderId &&
                                  od.ProductId == model.ProductId &&
                                  od.Order.CustomerId == userId);

        if (orderDetail == null)
        {
            TempData["Error"] = "Không tìm thấy đơn hàng!";
            return RedirectToAction("Index", "Order");
        }

        if (orderDetail.IsReviewed)
        {
      
            TempData["Error"] = "Sản phẩm này bạn đã đánh giá rồi.";
            return RedirectToAction("Detail", "Order", new { id = model.OrderId });
        }

        
        if (ModelState.IsValid)
        {
            var review = new Review
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                UserId = userId,
                Rating = model.Rating,
                Content = model.Content,
                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);

            orderDetail.IsReviewed = true;
            _context.Update(orderDetail);

            await _context.SaveChangesAsync();

            TempData["Message"] = "Đánh giá thành công!";
            return RedirectToAction("Detail", "Order", new { id = model.OrderId });
        }

        var product = _context.Products.Find(model.ProductId);
        if (product != null)
        {
            model.ProductName = product.Name;
            model.ProductImage = product.ImageUrl;
           
        }

      
        return View("Create", model);
    }
}