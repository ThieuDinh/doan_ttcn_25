using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        
        public string? Address { get; set; }
        public string? Gender { get; set; } 
        
    }
}