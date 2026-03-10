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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public ManagerUserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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
                    Role = string.Join(", ", roles),
                    IsLocked = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow,
                    HasOrder = _context.Orders.Any(a => a.CustomerId == user.Id)
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
        public async Task<IActionResult> Create(AdminCreateVM model)
        {
           
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FullName=model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.Roles))
                    {
                        if (await _roleManager.RoleExistsAsync(model.Roles))
                        {
                            await _userManager.AddToRoleAsync(user, model.Roles);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Roles);
            return View(model);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new ManagerUserVM
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault()
            };

            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManagerUserVM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();


            ModelState.Remove("UserName");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("Email");
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(model.NewPassword))
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resultPass = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (!resultPass.Succeeded)
                    {
                        foreach (var error in resultPass.Errors)
                            ModelState.AddModelError("", error.Description);
                        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);
                        return View(model);
                    }
                }

                var currentRoles = await _userManager.GetRolesAsync(user);

                string currentRole = currentRoles.FirstOrDefault();
                if (model.Role != currentRole)
                {
                    if (currentRoles.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    }

                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        if (await _roleManager.RoleExistsAsync(model.Role))
                        {
                            await _userManager.AddToRoleAsync(user, model.Role);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", model.Role);
            return View(model);

        }
        public async Task<IActionResult> Lock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

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
        public async Task<IActionResult> History(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var orders = await _context.Orders.Where(a => a.CustomerId == user.Id).Include(a => a.OrderDetails).ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = new AdminDeleteVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var id = Request.Form["Id"];
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            var model = new AdminDeleteVM
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View("Delete", model);
        }
    }
}