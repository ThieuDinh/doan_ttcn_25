using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using doan_ttcn.Models;
using doan_ttcn.Data;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.Models.ViewModels;

namespace doan_ttcn.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)  
    {
        _context = context;
    }

    public IActionResult Index(int? id)
    {   
        var product = _context.Products.AsQueryable();
        if(id.HasValue)
        {
            product = product.Where(p => p.CategoryId == id.Value);
        }
        var result=product.Select(p => new ProductViewModels
        {
            Id=p.Id,
            Name=p.Name,
            ImgUrl=p.ImageUrl,
            Price=p.Price
        });
        return View(result);
    }

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
