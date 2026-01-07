using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Thêm các thuộc tính bạn muốn mở rộng
        
        public string? FullName { get; set; }
        
        public string? Address { get; set; }
        public string? Gender { get; set; } 
        
    }
}