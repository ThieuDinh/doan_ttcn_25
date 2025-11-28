using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doan_ttcn.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "decimal(7,0)")]
        public decimal MinimumPrice { get; set; }
        [Column(TypeName = "decimal(7,0)")]
        public decimal DícountMax { get; set; }

        [Column(TypeName = "int")]
        public int DiscountPecent { get; set; }
        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}