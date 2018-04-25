namespace SocialNetwork.Data.Database
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Models.Models;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<PictureAlbum> PictureAlbums { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagAlbum> TagAlbums { get; set; }

        public DbSet<UserAlbum> UserAlbums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-924HS8U\SQLEXPRESS;Database=SocialNetwork;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .HasKey(f => new { f.MainUserId, f.FriendUserId });

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.MainUser)
                .WithMany(mu => mu.MainUserFriends)
                .HasForeignKey(f => f.MainUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.FriendUser)
                .WithMany(mu => mu.Friends)
                .HasForeignKey(f => f.FriendUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PictureAlbum>()
                .HasKey(pa => new {pa.AlbumId, pa.PictureId});

            modelBuilder.Entity<PictureAlbum>()
                .HasOne(p => p.Picture)
                .WithMany(a => a.Albums)
                .HasForeignKey(p => p.PictureId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PictureAlbum>()
                .HasOne(a => a.Album)
                .WithMany(p => p.Pictures)
                .HasForeignKey(a => a.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TagAlbum>()
                .HasKey(ta => new {ta.TagId, ta.AlbumId});

            modelBuilder.Entity<TagAlbum>()
                .HasOne(t => t.Tag)
                .WithMany(a => a.Albums)
                .HasForeignKey(t => t.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TagAlbum>()
                .HasOne(a => a.Album)
                .WithMany(t => t.Tags)
                .HasForeignKey(a => a.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAlbum>()
                .HasKey(ua => new {ua.UserId, ua.AlbumId});

            modelBuilder.Entity<UserAlbum>()
                .HasOne(u => u.User)
                .WithMany(a => a.SharedAlbums)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAlbum>()
                .HasOne(a => a.Album)
                .WithMany(u => u.Users)
                .HasForeignKey(a => a.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}