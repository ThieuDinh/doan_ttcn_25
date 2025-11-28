using Microsoft.AspNetCore.Identity;
using doan_ttcn.Models; // Namespace chứa ApplicationUser của bạn

namespace doan_ttcn.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // Lấy các dịch vụ cần thiết từ hệ thống
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            // 1. Tạo các Role (Vai trò) nếu chưa có
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
            await roleManager.CreateAsync(new IdentityRole("Manager"));
            await roleManager.CreateAsync(new IdentityRole("Staff"));
            await roleManager.CreateAsync(new IdentityRole("Customer"));

            // 2. Tạo tài khoản Admin mặc định
            var adminUser = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FullName = "Quản trị viên", // Đảm bảo model ApplicationUser có field này
                EmailConfirmed = true,
                PhoneNumber = "0999999999"
            };

            // Kiểm tra xem user này có trong DB chưa
            var userInDb = await userManager.FindByEmailAsync(adminUser.Email);
            if (userInDb == null)
            {
                // Mật khẩu mặc định: Admin@123
                await userManager.CreateAsync(adminUser, "Admin@123");
                // Gán quyền Admin
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}