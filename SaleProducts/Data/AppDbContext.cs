using Microsoft.EntityFrameworkCore;
using SaleProducts.Models;

namespace SaleProducts.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> LoginUsers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Printtest> Printtests  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Printtest>()
                .Property(p => p.SellingPrice)
                .HasColumnType("decimal(18,4)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
