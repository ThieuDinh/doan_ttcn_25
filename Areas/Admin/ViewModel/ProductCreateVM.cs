using System.ComponentModel.DataAnnotations;

namespace Areas.Admin.ViewModel;
public class ProductCreateVM
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    // Ở đây dùng IFormFile trực tiếp -> Không cần ImageUrl string
   
    public IFormFile? Photo { get; set; } 
}