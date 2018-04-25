namespace SocialNetwork.Client
{
    using System;
    using SocialNetwork.Data.Database;
    using System.Linq;
    using SocialNetwork.Data;
    using SocialNetwork.Models.Models;

    public class Program
    {
        public static void Main()
        {
            SocialNetworkDbContext db = new SocialNetworkDbContext();
            SeedDb seed = new SeedDb();
            using (db)
            {
                ClearDataBase(db);
                seed.SeedDatabase(db);

             //2. Friends --->
                // Task 1
                //ListAllUsersWithTheirFriends(db);

                // Task 2
                //ListActiveFriendsWithMoreThanFiveUsers(db);

             // 3. Albums --->
                // Task 1   
                //ListAlbumsWhitTheirOwner(db);

                // Task 2
                //ListPicturesIncludedInMoreThanTwoAlbums(db);

                // Task 3
                //ListAlbumsWithGivenId(db);

             // 4. Tags --->
                // Task 1
                //AddTagsToDatabase(db);
                //ListAlbumsWithGivenTag(db);

                // Task 2
                //AddTagsToDatabase(db);
                //ListUsersWithAlbumsWithMoreThan3Tags(db);

             // 5. Shared Albums
                // Task 1
                //PrintUsersWithSharedAlbums(db);

                // Task 2
                //ListAlbumsSharedWithMoreThanTwoPeople(db);

                // Task 3
                //ListAlbumsSharedWithUserWithGivenName(db);
            }
        }

        private static void ListAlbumsSharedWithUserWithGivenName(SocialNetworkDbContext db)
        {
            Console.WriteLine("Input Username...");
            var userName = Console.ReadLine();
            var albums = db.Albums
                .Where(a => a.Users.Any(e => e.User.Username == userName))
                .Select(a => new
                {
                    a.Name,
                    CountOfPictures = a.Pictures.Count
                })
                .OrderByDescending(c => c.CountOfPictures)
                .ThenBy(n => n.Name)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album Name: {album.Name}");
                Console.WriteLine($"Count Of Pictures: {album.CountOfPictures}");
            }
        }

        private static void ListAlbumsSharedWithMoreThanTwoPeople(SocialNetworkDbContext db)
        {
            var albums = db.Albums
                .Where(a => a.Users.Count > 2)
                .Select(al => new
                {
                    al.Name,
                    UsersCount = al.Users.Count,
                    al.IsPublic
                })
                .OrderByDescending(u => u.UsersCount)
                .ThenBy(n => n.Name)
                .ToList();
            
            foreach (var album in albums)
            {
                Console.WriteLine($"Album Name: {album.Name}");
                Console.WriteLine($"Count Of Users: {album.UsersCount}");
                Console.WriteLine($"Is Public: {album.IsPublic}");
            }
        }

        private static void PrintUsersWithSharedAlbums(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    Friends = u.Friends
                        .Select(f => new
                        {
                            Name = f.FriendUser.Username,
                            SharedAlbums = f
                                .FriendUser
                                .SharedAlbums
                                .Select(s => s.Album.Name)
                        })
                })
                .OrderBy(u => u.Username)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username}");

                foreach (var friend in user.Friends)
                {
                    Console.WriteLine($"Friend: {friend.Name}; Shared Albums: {string.Join(", ", friend.SharedAlbums)}");
                }

                Console.WriteLine();
            }
        }

        private static void ListUsersWithAlbumsWithMoreThan3Tags(SocialNetworkDbContext db)
        {
            var users = db.Users
                .Where(a => a.Albums.Count > 0)
                .Where(t => t.Albums.Any(e => e.Tags.Count > 3))
                .Select(u => new
                {
                    u.Username,
                    AlbumTitle = u.Albums
                        .Select(s => new
                        {
                            s.Name,
                            Tags = s.Tags
                                .Select(e => e.Tag.Name)
                        }),
                    AlbumCount = u.Albums.Count,
                    TagCount = u.Albums.Select(t => t.Tags.Count)
                })
                .OrderByDescending(t => t.AlbumCount)
                .ThenByDescending(t => t.TagCount)
                .ThenBy(n => n.Username)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username}");
                foreach (var album in user.AlbumTitle)
                {
                    Console.WriteLine($"{album.Name}");
                    foreach (var tag in album.Tags)
                    {
                        Console.WriteLine($"{tag}");
                    }
                }
            }
        }

        private static void ListAlbumsWithGivenTag(SocialNetworkDbContext db)
        {
            Console.WriteLine("Insert The Tag....");
            var inputTag = Console.ReadLine();
            var albums = db.Albums
                .Where(t => t.Tags.Any(n => n.Tag.Name == TagTransformer.Transform(inputTag)))
                .Select(a => new
                {
                    a.Name,
                    Owner = a.Users.Select(u => u.User.Username),
                    NumberOfTags = a.Tags.Count
                })
                .OrderByDescending(t => t.NumberOfTags)
                .ThenBy(n => n.Name);

            foreach (var album in albums)
            {
                Console.WriteLine($"Album Title: {album.Name}");
                Console.WriteLine($"Owner Name: {album.Owner}");
                Console.WriteLine();
            }
        }

        private static void AddTagsToDatabase(SocialNetworkDbContext db)
        {
            Console.WriteLine("Write 4 tags");
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Tag {i+1}: ");
                var input = Console.ReadLine();
                db.Tags.Add(new Tag
                {
                    Name = TagTransformer.Transform(input)
                });
            }
            db.SaveChanges();

            var albumCount = db.Albums.Count();

            for (int i = 0; i < albumCount; i++)
            {
                db.TagAlbums.Add(new TagAlbum
                {
                    TagId = i + 1,
                    AlbumId = i + 1
                });
                db.SaveChanges();
            }
            var tagCount = db.Tags.Count();
            for (int i = 0; i < tagCount - 1; i++)
            {
                db.TagAlbums.Add(new TagAlbum
                {
                    TagId = i + 2,
                    AlbumId = 1
                });
                db.SaveChanges();
            }
        }

        private static void ListAlbumsWithGivenId(SocialNetworkDbContext db)
        {
            var id = 2;

            var albums = db.Albums
                .Where(u => u.UserId == id)
                .Select(a => new
                {
                    Username = a.User.Username,
                    AlbumName = a.Name,
                    IsPublic = a.IsPublic,
                    Pictures = a.Pictures
                        .Select(p => new
                        {
                            PictureTitle = p.Picture.Title,
                            Path = p.Picture.Path
                        }),
                    Message = "Private Content!"
                })
                .OrderBy(n => n.AlbumName)
                .ToList();

            Console.WriteLine($"User: {db.Users.FirstOrDefault(u => u.Id == id).Username}");
            foreach (var album in albums)
            {
                if (album.IsPublic)
                {
                    Console.WriteLine($"Album Name: {album.AlbumName}");
                    foreach (var pic in album.Pictures)
                    {
                        Console.WriteLine($"Picture Name: {pic.PictureTitle}");
                        Console.WriteLine($"Picture Path: {pic.Path}");
                    }
                }
                else
                {
                    Console.WriteLine($"Album Name: {album.AlbumName}");
                    Console.WriteLine($"{album.Message}");
                }
            }
        }

        private static void ListPicturesIncludedInMoreThanTwoAlbums(SocialNetworkDbContext db)
        {
            var pictures = db.Pictures
                .Where(a => a.Albums.Count > 2)
                .Select(p => new
                {
                    p.Title,
                    Albums = p.Albums
                             .Select(a => new
                        {
                            AlbumName = a.Album.Name,
                            OwnerName = a.Album.User.Username
                        }),
                    p.Albums.Count
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(n => n.Title)
                .ToList();

            foreach (var picture in pictures)
            {
                Console.WriteLine($"Title: {picture.Title}");
                foreach (var album in picture.Albums)
                {
                    Console.WriteLine($"Album Name: {album.AlbumName}");
                    Console.WriteLine($"Owner Name: {album.OwnerName}");
                }
            }
        }

        private static void ListAlbumsWhitTheirOwner(SocialNetworkDbContext db)
        {
            var albums = db.Albums
                .Select(t => new
                {
                    t.Name,
                    Owner = t.User.Username,
                    t.Pictures.Count
                })
                .OrderByDescending(c => c.Count)
                .ThenBy(n => n.Owner)
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"Album Title: {album.Name}");
                Console.WriteLine($"Owner Name: {album.Owner}");
                Console.WriteLine($"Count Of Pictures: {album.Count}");
            }
        }

        private static void ListActiveFriendsWithMoreThanFiveUsers(SocialNetworkDbContext db)
        {
            var activeUsers = db.Users
                .Where(a => a.IsDeleted == false)
                .Where(f => f.Friends.Count > 5)
                .Select(u => new
                {
                    u.Username,
                    u.RegisteredOn,
                    FriendsCount = u.Friends.Count,
                    Period = u.RegisteredOn.Subtract(u.LastTimeLoggedIn).TotalDays
                })
                .OrderBy(r => r.RegisteredOn)
                .ThenByDescending(c => c.FriendsCount)
                .ToList();

            foreach (var user in activeUsers)
            {
                Console.WriteLine($"{user.Username} with {user.FriendsCount} friends, active {(int)user.Period} days");
            }
        }

        private static void ListAllUsersWithTheirFriends(SocialNetworkDbContext db)
        {
            var users = db.Users
                .Where(s => s.IsDeleted == false)
                .Select(u => new
                {
                    u.Username,
                    FriendsCount = u.Friends.Count
                })
                .OrderByDescending(f => f.FriendsCount)
                .ThenBy(n => n.Username)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} with {user.FriendsCount} friends.");
            }
        }

        private static void ClearDataBase(SocialNetworkDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
