namespace SocialNetwork.Data.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SocialNetwork.Models.Models;

    public class SeedDb
    {
        public void SeedDatabase(SocialNetworkDbContext db)
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                User user = new User
                {
                    Username = $"Unit {i + 1}",
                    Age = 48,
                    Email = $"{(char)rnd.Next(97, 122)}{(char)rnd.Next(97, 122)}{(char)rnd.Next(97, 122)}@sql.bg",
                    Password = PasswordGenerator(),
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now.AddDays(rnd.Next(50, 1000)),
                    LastTimeLoggedIn = DateTime.Now.AddDays(rnd.Next(-3, -1))
                };
                db.Users.Add(user);
            }
            db.SaveChanges();
            this.MakeUsersFriends(db);

            this.MakeAlbumsAndPictures(db);
        }

        private void MakeAlbumsAndPictures(SocialNetworkDbContext db)
        {
            Picture pic1 = new Picture
            {
                Title = "Pic1",
                Caption = "Default",
                Path = @"C:\MyPics"
            };

            Picture pic2 = new Picture
            {
                Title = "Pic2",
                Caption = "Default",
                Path = @"C:\MyPics"
            };

            Picture pic3 = new Picture
            {
                Title = "Pic3",
                Caption = "Default",
                Path = @"C:\MyPics"
            };

            Picture pic4 = new Picture
            {
                Title = "Pic4",
                Caption = "Default",
                Path = @"C:\MyPics"
            };

            Picture pic5 = new Picture
            {
                Title = "Pic5",
                Caption = "Default",
                Path = @"C:\MyPics"
            };

            Album album1 = new Album
            {
                BackgroundColor = "Blue",
                Name = "SomeName",
                IsPublic = true,
                UserId = 1
            };

            Album album2 = new Album
            {
                BackgroundColor = "Green",
                Name = "PeshosAlbum",
                IsPublic = true,
                UserId = 2
            };

            Album album3 = new Album
            {
                BackgroundColor = "White",
                Name = "Fun",
                IsPublic = true,
                UserId = 3
            };

            db.Albums.Add(album1);
            db.Albums.Add(album2);
            db.Albums.Add(album3);

            db.Pictures.Add(pic1);
            db.Pictures.Add(pic2);
            db.Pictures.Add(pic3);
            db.Pictures.Add(pic4);
            db.Pictures.Add(pic5);

            db.SaveChanges();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    PictureAlbum pictureAlbum = new PictureAlbum
                    {
                        AlbumId = i+1,
                        PictureId = j+1
                    };
                    db.PictureAlbums.Add(pictureAlbum);
                }
            }
            db.SaveChanges();
        }

        private static string PasswordGenerator()
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                sb.Append($"{(char)rnd.Next(97, 122)}");
            }

            return sb.ToString().Trim();
        }

        private void MakeUsersFriends(SocialNetworkDbContext db)
        {
            Random rnd = new Random();

            var userIds = db.Users.Select(u => u.Id).ToList();

            for (int i = 0; i < userIds.Count; i++)
            {
                var currentUserId = userIds[i];

                var totalFriends = 2;

                for (int j = 0; j < totalFriends; j++)
                {
                    var friendId = userIds[rnd.Next(0, userIds.Count)];
                    bool validFriendship = friendId != currentUserId;

                    var friendshipExists =
                        db.Friends.Any(f => (f.MainUserId == currentUserId && f.FriendUserId == friendId) ||
                                                (f.MainUserId == friendId && f.FriendUserId == currentUserId));

                    if (friendshipExists)
                    {
                        validFriendship = false;
                    }

                    if (!validFriendship)
                    {
                        j--;
                        continue;
                    }

                    db.Friends.Add(new Friend
                    {
                        MainUserId = currentUserId,
                        FriendUserId = friendId
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}