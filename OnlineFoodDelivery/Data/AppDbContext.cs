using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data.Models;

namespace OnlineFoodDelivery.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Food> Foods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Query filter për User
            builder.Entity<User>().HasQueryFilter(u => u.isActive);

            // Orders → Client
            builder.Entity<Orders>()
                .HasOne(o => o.Client)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Orders → Driver
            builder.Entity<Orders>()
                .HasOne(o => o.Driver)
                .WithMany()
                .HasForeignKey(o => o.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem → Order
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem → Food
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Food)
                .WithMany(f => f.OrderItems)
                .HasForeignKey(oi => oi.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            // Food → Restaurant
            builder.Entity<Food>()
                .HasOne(f => f.Restaurant)
                .WithMany(r => r.Foods)
                .HasForeignKey(f => f.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review → Client
            builder.Entity<Review>()
                .HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Review → Restaurant
            builder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(rst => rst.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
