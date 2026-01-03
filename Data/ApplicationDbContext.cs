using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace doan_ttcn.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<doan_ttcn.Models.Category> Categories { get; set; } = default!;

        public DbSet<doan_ttcn.Models.Product> Products { get; set; } = default!;
        public DbSet<doan_ttcn.Models.ProductImage> ProductImages { get; set; } = default!;
        public DbSet<doan_ttcn.Models.ProductBatch> ProductBatches { get; set; } = default!;
        public DbSet<doan_ttcn.Models.Order> Orders { get; set; } = default!;
        public DbSet<doan_ttcn.Models.OrderDetail> OrderDetails { get; set; } = default!;
        public DbSet<doan_ttcn.Models.Cart> Carts { get; set; }
        public DbSet<doan_ttcn.Models.CartItem> CartItems { get; set; }
        public DbSet<doan_ttcn.Models.Voucher> Vouchers { get; set; }
    }
}
