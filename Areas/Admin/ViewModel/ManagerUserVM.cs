using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace doan_ttcn.Areas.Admin.ViewModel;

public class ManagerUserVM
{
    public string Id { get; set; }
    public string UserName { get; set; }


    public string Email { get; set; }


    public string PhoneNumber { get; set; }


    public string Role { get; set; } // Dùng cho trang Index

    // Dùng cho trang Edit
    public bool IsLocked { get; set; }

   
    public string? NewPassword { get; set; } // Nếu để trống thì không đổi pass


    public List<SelectListItem>? Roles { get; set; }
    public bool HasOrder { get; set; }
}