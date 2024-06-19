using Microsoft.EntityFrameworkCore;
using MiniMarket_API.Model.Entities;
using System.Text;

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
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<CompanyCode> EmployeeCodes { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<SaleOrder> Orders { get; set; }
        public DbSet<OrderDetails> Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);

            //modelBuilder.Entity<SuperAdmin>().HasData(NewDefaultAdminSeed());

            modelBuilder.Entity<SaleOrder>()
                .HasMany(s => s.Details)
                .WithOne(d => d.SaleOrder)
                .HasForeignKey(d => d.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Details)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DeliveryAddress>()
                .HasMany(d => d.SaleOrders)
                .WithOne(s => s.DeliveryAddress)
                .HasForeignKey(s => s.AddressId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private SuperAdmin NewDefaultAdminSeed()
        {
            var result = new SuperAdmin
            {
                Id = Guid.NewGuid(),
                Name = "Admin Default",
                Email = "admin@example.com",
                PasswordHash = Encoding.UTF8.GetBytes("f0de3280c8f226ce260bd61a13098692881d2626dd5b2c62b1f49dfb35f5a609"),
                PhoneNumber = "+549999999999",
            };

            return result;
        }
    }
}
