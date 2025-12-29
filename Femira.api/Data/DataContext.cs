using Femira.api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Femira.api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------
            // Order → OrderItems
            // --------------------
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // --------------------
            // User → Orders
            // --------------------
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.User_Id);

            // --------------------
            // User → UserAddress
            // --------------------
            modelBuilder.Entity<UserAddress>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.User_Id);

            // --------------------
            // Decimal precision
            // --------------------
            modelBuilder.Entity<Order>()
                .Property(o => o.Total_Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.P_Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Unit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.P_Price)
                .HasPrecision(18, 2);

            // --------------------
            // Seed data
            // --------------------
            modelBuilder.Entity<Product>()
                .HasData(Product.GetSeedData());
        }
    }
}
