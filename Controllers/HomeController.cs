using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using doan_ttcn.Models;
using doan_ttcn.Data;
using Microsoft.EntityFrameworkCore;

namespace doan_ttcn.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var topSales = _context.OrderDetails
            .Include(od => od.Product)
            .Where(od => od.Product != null)
            .GroupBy(od => od.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                ProductName = g.FirstOrDefault().Product.Name,
                ImageUrl = $"product/{g.FirstOrDefault().Product.ImageUrl}", 
                TotalSold = g.Sum(od => od.Quantity),
                Rating = _context.Reviews
                    .Where(r => r.ProductId == g.Key)
                    .Average(r => (double?)r.Rating) ?? 0
            })
            .OrderByDescending(x => x.TotalSold)
            .Take(3)
            .ToList();

        ViewData["TopSales"] = topSales;

        var reviews = _context.Reviews
        .Include(r => r.User)
        .Include(r => r.Product)
        .OrderByDescending(r => r.CreatedAt)
        .Take(5)
        .Select(r => new 
        {
            Id = r.Id,
            Rating = r.Rating,
            Content = r.Content,
            CreatedAt = r.CreatedAt,
            UserName = r.User.UserName,
            ProductName = r.Product.Name
        })
        .ToList();

    ViewBag.Reviews = reviews;

        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
