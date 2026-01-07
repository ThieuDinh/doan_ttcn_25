using doan_ttcn.Models;
using doan_ttcn.ViewModels;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        // 2. Sử dụng Dependency Injection (DI) để nhận các dịch vụ
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

       
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
               
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, 
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Đăng nhập thất bại. Vui lòng kiểm tra lại Email và Mật khẩu.");
            }
         
            return View(model);
        }

       
        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = "", 
                    Address = "", 
                    PhoneNumber = ""
                };


                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                  
                    if (!await _roleManager.RoleExistsAsync("Customer"))
                    {
                        
                        await _roleManager.CreateAsync(new IdentityRole("Customer"));
                    }

                    
                    await _userManager.AddToRoleAsync(user, "Customer");

                  

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

              
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserInfo
            {
                UserId = user.Id,
                Fullname = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
               
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInfo(UserInfo model)
        {
          
            ModelState.Remove("Password");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login");

                // Cập nhật thông tin
                user.FullName = model.Fullname;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin cá nhân thành công!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("Index", model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
        
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

        
            var model = new UserInfo
            {
                UserId = user.Id
            };

        
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserInfo model)
        {
           
            ModelState.Remove("Fullname");
            ModelState.Remove("Address");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("UserId");

           
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login");

               
                var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

                if (result.Succeeded)
                {
                    
                    await _signInManager.RefreshSignInAsync(user);

                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                    return RedirectToAction(nameof(Index));
                }

                // Nếu thất bại (sai pass cũ, pass mới yếu...)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.Fullname = currentUser.FullName;
                model.Address = currentUser.Address;
                model.PhoneNumber = currentUser.PhoneNumber;
                model.UserId = currentUser.Id;
            }

            // Trả về view Index kèm thông báo lỗi
            return View("ChangePassword", model);
        }

    }
}