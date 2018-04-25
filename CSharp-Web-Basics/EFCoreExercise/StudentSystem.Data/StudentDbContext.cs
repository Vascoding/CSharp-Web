namespace StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Models;

    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=StudentDatabase;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            modelBuilder.Entity<Student>()
                .HasMany(sc => sc.Courses)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<Course>()
                .HasMany(sc => sc.Students)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(r => r.Resources)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(h => h.Homeworks)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<Student>()
                .HasMany(h => h.Homeworks)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<Resource>()
                .HasMany(l => l.Licenses)
                .WithOne(r => r.Resource)
                .HasForeignKey(r => r.ResourceId);
        }
    }
}