
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CameraStore.Data.Models;

namespace CameraStore.Data
{
    public class CameraDbContext : IdentityDbContext<User>
    {
        public DbSet<Camera> Cameras { get; set; }
        
        public CameraDbContext(DbContextOptions<CameraDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(c => c.Cameras)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);


            base.OnModelCreating(builder);
        }
    }
}
