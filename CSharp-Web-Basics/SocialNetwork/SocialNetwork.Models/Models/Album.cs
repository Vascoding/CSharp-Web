namespace SocialNetwork.Models.Models
{
    using System.Collections.Generic;

    public class Album
    {
        public Album()
        {
            this.Pictures = new List<PictureAlbum>();
            this.Tags = new List<TagAlbum>();
            this.Users = new List<UserAlbum>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public List<PictureAlbum> Pictures { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<TagAlbum> Tags { get; set; }

        public List<UserAlbum> Users { get; set; }
    }
}