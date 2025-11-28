using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(7,0)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "nvarchar(500)")]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}