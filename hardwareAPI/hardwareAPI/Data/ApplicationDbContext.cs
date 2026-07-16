using hardwareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace hardwareAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<SaleItem> SaleItems { get; set; }
    }
}
