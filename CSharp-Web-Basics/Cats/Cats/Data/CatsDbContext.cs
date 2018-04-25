namespace Cats.Data
{
    using Cats.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CatsDbContext : DbContext
    {
        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
    }
}