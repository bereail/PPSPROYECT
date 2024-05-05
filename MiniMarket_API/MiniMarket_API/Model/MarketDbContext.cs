using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Model.Entities;

namespace MiniMarket_API.Model
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CompanyCode> EmployeeCodes { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleOrder> Orders { get; set; }
        public DbSet<OrderDetails> Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);

            modelBuilder.Entity<SaleOrder>()
                .HasMany(s => s.Details)
                .WithOne(d => d.SaleOrder)
                .HasForeignKey(d => d.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Details)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId);
        }
    }
}
