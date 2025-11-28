using System.ComponentModel.DataAnnotations;

namespace doan_ttcn.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } 
        public decimal DiscountValue { get; set; } 
        public int Quantity { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}