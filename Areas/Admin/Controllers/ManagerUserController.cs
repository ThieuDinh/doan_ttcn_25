using doan_ttcn.Areas.Admin.ViewModel;
using doan_ttcn.Data;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ManagerUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManagerUserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/ManagerUser
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<ManagerUserVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new ManagerUserVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = string.Join(", ", roles), // Lấy danh sách role gộp thành chuỗi
                    IsLocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow
                });
            }
            return View(userViewModels);
        }
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagerUserVM model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserName");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.NewPassword);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        if (await _roleManager.RoleExistsAsync(model.Role))
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);
            return View(model);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();

            // 1. Tìm user theo Id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // 2. Lấy Role hiện tại của user
            var roles = await _userManager.GetRolesAsync(user);

            // 3. Đổ dữ liệu vào ViewModel
            var model = new ManagerUserVM
            {
                Id = user.Id,
                Email = user.Email, // Chỉ để hiển thị (disabled)
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault() // Lấy role đầu tiên (nếu có)
            };

            // 4. Tạo danh sách Role cho Dropdown, chọn sẵn role hiện tại
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManagerUserVM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            // Bỏ qua validate các trường không cần thiết hoặc readonly
            ModelState.Remove("UserName");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("Email");
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                // 1. Xử lý Đổi Mật Khẩu (Nếu admin có nhập mật khẩu mới)
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    // Vì Admin đổi pass cho User nên không cần biết pass cũ.
                    // Ta dùng Token để Reset mật khẩu.
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resultPass = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (!resultPass.Succeeded)
                    {
                        foreach (var error in resultPass.Errors)
                            ModelState.AddModelError("", error.Description);

                        // Load lại role và trả về View nếu lỗi pass
                        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);
                        return View(model);
                    }
                }

                // 2. Xử lý Đổi Role
                // Lấy role cũ đang có
                var currentRoles = await _userManager.GetRolesAsync(user);

                // So sánh: Nếu Role chọn trên form KHÁC Role đang có thì mới cập nhật
                string currentRole = currentRoles.FirstOrDefault();
                if (model.Role != currentRole)
                {
                    // Xóa role cũ
                    if (currentRoles.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    }

                    // Thêm role mới (nếu có chọn)
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        // Kiểm tra role tồn tại trước khi add
                        if (await _roleManager.RoleExistsAsync(model.Role))
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }
                    }
                }

                // 3. Cập nhật thành công -> Về trang danh sách
                return RedirectToAction(nameof(Index));
            }

            // Nếu Model không valid, load lại view
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);
            return View(model);

        }
        public async Task<IActionResult> Lock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Đặt LockoutEnd thành thời gian trong tương lai để khóa tài khoản
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unlock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);

            return RedirectToAction(nameof(Index));
        }
    }
}