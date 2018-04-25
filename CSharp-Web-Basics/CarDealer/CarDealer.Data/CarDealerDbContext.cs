namespace CarDealer.Data
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class CarDealerDbContext : DbContext
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(s => s.Sales)
                .WithOne(c => c.Customer);

            modelBuilder.Entity<Supplier>()
                .HasMany(p => p.Parts)
                .WithOne(s => s.Supplier)
                .HasForeignKey(s => s.Supplier_Id);

            modelBuilder.Entity<PartCars>()
                .HasKey(pc => new {pc.Part_Id, pc.Car_Id });

            modelBuilder.Entity<Car>()
                .HasMany(p => p.Parts)
                .WithOne(c => c.Car)
                .HasForeignKey(c => c.Car_Id);

            modelBuilder.Entity<Part>()
                .HasMany(c => c.Cars)
                .WithOne(p => p.Part)
                .HasForeignKey(p => p.Part_Id);

            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
