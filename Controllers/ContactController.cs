using Microsoft.AspNetCore.Mvc;
namespace doan_ttcn.Controllers;
public class ContactController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Contact(string Name, string Email, string Message)
    {
        // Demo: chưa lưu DB
        TempData["Success"] = "Gửi liên hệ thành công! Chúng tôi sẽ phản hồi sớm.";
        return RedirectToAction("Contact");
    }
}