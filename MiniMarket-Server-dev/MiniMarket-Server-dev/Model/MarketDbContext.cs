using Microsoft.EntityFrameworkCore;
using MiniMarket_Server_dev.Model.Entities;

namespace MiniMarket_Server_dev.Model
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
        }
    }
}
