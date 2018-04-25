namespace Shop.Database
{
    using Microsoft.EntityFrameworkCore;
    using Shop.Models;

    public class ShopDbContext : DbContext
    {
        public DbSet<Salesman> Salesmen { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemOrder> ItemOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=ShopDbContext;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(s => s.Salesman)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.SalesmanId);

            modelBuilder.Entity<Order>()
                .HasOne(c => c.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne(c => c.Customer)
                .WithMany(r => r.Reviews)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder.Entity<ItemOrder>()
                .HasKey(key => new {key.ItemId, key.OrderId});

            modelBuilder.Entity<Item>()
                .HasMany(o => o.Orders)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId);

            modelBuilder.Entity<Order>()
                .HasMany(i => i.Items)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Item>()
                .HasMany(r => r.Reviews)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId);
        }
    }
}