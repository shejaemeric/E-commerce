using Microsoft.EntityFrameworkCore;
using E_Commerce_Api.Models;
using System.Linq;
using System.Data;

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
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ShoppingSession> ShoppingSessions { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }


        // Archives


        public DbSet<CartItem_Archive> CartItems_Archive { get; set; }
        public DbSet<Discount_Archive> Discounts_Archive { get; set; }
        public DbSet<Inventory_Archive> Inventories_Archive { get; set; }
        public DbSet<OrderDetail_Archive> OrderDetails_Archive { get; set; }
        public DbSet<OrderItem_Archive> OrderItems_Archive { get; set; }
        public DbSet<PaymentDetail_Archive> PaymentDetails_Archive { get; set; }
        public DbSet<Product_Archive> Products_Archive { get; set; }
        public DbSet<ProductCategory_Archive> ProductCategories_Archive { get; set; }
        public DbSet<ShoppingSession_Archive> ShoppingSessions_Archive { get; set; }
        public DbSet<UserAddress_Archive> UserAddresses_Archive { get; set; }
        public DbSet<UserPayment_Archive> UserPayments_Archive { get; set; }
        public DbSet<User_Archive> Users_Archive { get; set; }

        public DbSet<Role_Archive> Roles_Archive { get; set; }
        public DbSet<Permission_Archive> Permissions_Archive { get; set; }

        public DbSet<RolePermission_Archive> RolesPermissions_Archive { get; set; }
        public DbSet<PasswordResetToken_Archive> PasswordResetTokens_Archive { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

                modelBuilder.Entity<Discount>()
                .Property(d => d.Discount_percent)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

                modelBuilder.Entity<ShoppingSession>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18, 2)");

                modelBuilder.Entity<CartItem>()
                .Property(s => s.Sub_total)
                .HasColumnType("decimal(18, 2)");

                // Archives


            modelBuilder.Entity<Discount_Archive>()
                .Property(d => d.Discount_percent)
                .HasColumnType("decimal(18, 2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<ShoppingSession_Archive>()
                .Property(s => s.Total)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<CartItem_Archive>()
                .Property(s => s.Sub_total)
                .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<Product>().Property(p => p.DiscountId).IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.ProductCategoryId).IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.InventoryId).IsRequired(false);

            modelBuilder.Entity<Product>()
                    .HasOne(p => p.ProductCategory)
                    .WithMany(p=>p.Products)
                    .HasForeignKey(p => p.ProductCategoryId)
                    .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                    .HasOne(p => p.Inventory)
                    .WithMany(p=>p.Products)
                    .HasForeignKey(p => p.InventoryId)
                    .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                    .HasOne(p => p.Discount)
                    .WithMany(p=>p.Products)
                    .HasForeignKey(p => p.DiscountId)
                    .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
