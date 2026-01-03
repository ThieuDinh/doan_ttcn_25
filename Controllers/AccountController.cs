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
                    await _userManager.AddToRoleAsync(user, "Customer");

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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Lấy user đang đăng nhập hiện tại
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Map dữ liệu từ ApplicationUser (Entity) sang UserInfo (ViewModel)
            var model = new UserInfo
            {
                UserId = user.Id,
                Fullname = user.FullName, // Lưu ý: Model ApplicationUser viết là FullName
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                // Các trường Password để trống để người dùng nhập nếu cần đổi
            };

            return View(model); // Trả về view UserInfo.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInfo(UserInfo model)
        {
            // Bỏ qua validate các trường mật khẩu vì form này không gửi lên
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
                    return RedirectToAction(nameof(Index)); // Load lại trang sạch sẽ
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Nếu lỗi, trả về view Index để hiện lỗi
            return View("Index", model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            // 1. Lấy thông tin user hiện tại đang đăng nhập
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // 2. Tạo model để truyền UserId sang View (quan trọng vì View của bạn có dòng asp-for="UserId")
            var model = new UserInfo
            {
                UserId = user.Id
            };

            // 3. Trả về View ChangePassword.cshtml
            return View(model);
        }
        // ---------------------------------------------------------
        // 3. ACTION CHANGEPASSWORD: Đổi mật khẩu
        // ---------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserInfo model)
        {
            // Bỏ qua validate các trường thông tin cá nhân
            ModelState.Remove("Fullname");
            ModelState.Remove("Address");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("UserId");

            // Kiểm tra xác nhận mật khẩu (nếu ViewModel chưa có DataAnnotation [Compare])
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Login");

                // Hàm ChangePasswordAsync tự động kiểm tra:
                // 1. Password cũ có đúng không?
                // 2. Password mới có đủ mạnh không?
                var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

                if (result.Succeeded)
                {
                    // Quan trọng: Refresh lại session để không bị đăng xuất
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

            // --- QUAN TRỌNG: Nạp lại thông tin cũ ---
            // Vì khi return View, các ô bên "Thông tin chung" sẽ bị rỗng do form password không gửi dữ liệu đó lên.
            // Ta cần lấy lại từ DB để hiển thị cho đẹp.
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