namespace ManyToManyRelation.Database
{
    using ManyToManyRelation.Models;
    using Microsoft.EntityFrameworkCore;
    public class UniDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=UniDbContext;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            modelBuilder.Entity<Student>()
                .HasMany(c => c.Courses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(st => st.StudentId);

            modelBuilder.Entity<Course>()
                .HasMany(s => s.Students)
                .WithOne(sc => sc.Course)
                .HasForeignKey(c => c.CourseId);
        }
    }
}