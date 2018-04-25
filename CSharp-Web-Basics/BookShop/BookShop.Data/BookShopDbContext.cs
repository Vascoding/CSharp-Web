using BookShop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopDbContext : DbContext
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options)
            :base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            builder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(bc => bc.BookId);

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(bc => bc.CategoryId);

            base.OnModelCreating(builder);
        }
    }
}
