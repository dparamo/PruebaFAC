using Microsoft.EntityFrameworkCore;
using PruebaFAC.Entities;

namespace PruebaFAC.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order!)
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<OrderItem>()
                .Property(i => i.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
