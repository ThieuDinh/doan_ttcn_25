using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<doan_ttcn.Models.Category> Categories { get; set; } = default!;

    public DbSet<doan_ttcn.Models.Product> Products { get; set; } = default!;
}
