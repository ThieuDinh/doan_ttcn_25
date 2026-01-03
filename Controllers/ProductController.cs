using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using doan_ttcn.Models;
using doan_ttcn.Data;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.ViewModels;

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
            Price=p.Price,
           CategoryName=p.CategoryName
        });
        return View( result);
    }
    public IActionResult Detail(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = _context.Products
            .FirstOrDefault(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        var result=new ProductDetailVM
        {
            Name=product.Name,
            ImgUrl=product.ImageUrl,
            Price=product.Price,
            Description=product.Description,
            CategoryName=product.CategoryName,
            Rate=5
        };
        return View(result);
    }
    public IActionResult Search (string? query)
    {
        var products = _context.Products
            .Where(p => p.Name.Contains(query))
            .Select(p => new ProductViewModels
            {
                Id = p.Id,
                Name = p.Name,
                ImgUrl = p.ImageUrl,
                Price = p.Price,
                CategoryName = p.CategoryName
            })
            .ToList();

        return View("Index", products);
    }
   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
