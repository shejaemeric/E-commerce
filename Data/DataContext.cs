using Microsoft.EntityFrameworkCore;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ShoppingSession> ShoppingSessions { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<User>()
                .HasOne(u => u.UserAddress)
                .WithOne(ua => ua.User)
                .HasForeignKey<UserAddress>(ua => ua.UserId);

                modelBuilder.Entity<User>()
                .HasOne(u => u.UserPayment)
                .WithOne(ua => ua.User)
                .HasForeignKey<UserPayment>(up => up.UserId);

                modelBuilder.Entity<Discount>()
                .Property(d => d.Discount_percent)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

                modelBuilder.Entity<ShoppingSession>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
