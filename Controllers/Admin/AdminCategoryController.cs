using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using doan_ttcn.Models;

namespace doan_ttcn.Controllers;
[Route("admin")]
public class AdminCategoryController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AdminCategoryController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
[Route("index")]
    public IActionResult Index()
    {
        return View();
    }

    
}
