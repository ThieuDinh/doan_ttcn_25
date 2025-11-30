using doan_ttcn.Models;
using doan_ttcn.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// Đảm bảo namespace này khớp với thư mục Controllers của bạn
namespace doan_ttcn.Controllers
{
    public class AccountController : Controller
    {
        // 1. Khai báo các dịch vụ Identity cần dùng
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // 2. Sử dụng Dependency Injection (DI) để nhận các dịch vụ
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- HÀNH ĐỘNG ĐĂNG NHẬP ---

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); // Trả về Views/Account/Login.cshtml
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                // Gọi dịch vụ Identity để xác thực User/Password
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, // Identity sử dụng Username/Email để xác thực
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true); 

                if (result.Succeeded)
                {
                    // Đăng nhập thành công: Chuyển hướng về trang chủ hoặc URL được yêu cầu
                    return RedirectToLocal(returnUrl); 
                }

                // Nếu xác thực thất bại
                ModelState.AddModelError(string.Empty, "Đăng nhập thất bại. Vui lòng kiểm tra lại Email và Mật khẩu.");
            }
            // Trả về View nếu ModelState không hợp lệ hoặc đăng nhập thất bại
            return View(model); 
        }

        // --- HÀNH ĐỘNG ĐĂNG KÝ ---

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Trả về Views/Account/Register.cshtml
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tạo một IdentityUser mới
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                // Dùng UserManager để tạo User và Hash mật khẩu
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Tùy chọn: Gán Role "User" (hoặc "Customer") mặc định cho User mới tạo
                    // await _userManager.AddToRoleAsync(user, "User"); 

                    // Đăng nhập User ngay lập tức và chuyển hướng
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); 
                }

                // Nếu có lỗi khi tạo user (ví dụ: mật khẩu yếu)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Trả về View nếu ModelState không hợp lệ
            return View(model);
        }

        // --- HÀNH ĐỘNG ĐĂNG XUẤT ---

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // --- HÀM HỖ TRỢ ---

        // Hàm hỗ trợ chuyển hướng an toàn (tránh các URL độc hại)
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}