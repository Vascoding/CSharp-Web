namespace OneToManyRelation.Database
{
    using Microsoft.EntityFrameworkCore;
    using OneToManyRelation.Models;

    public class CompanyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=CompanyDatabase;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(d => d.DepartmentId);

            modelBuilder.Entity<Employee>()
                .HasOne(mngr => mngr.Manager)
                .WithMany(e => e.Employees)
                .HasForeignKey(m => m.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}