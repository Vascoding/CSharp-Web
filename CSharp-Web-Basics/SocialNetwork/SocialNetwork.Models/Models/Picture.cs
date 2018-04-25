namespace SocialNetwork.Models.Models
{
    using System.Collections.Generic;

    public class Picture
    {
        public Picture()
        {
            this.Albums = new List<PictureAlbum>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Path { get; set; }

        public List<PictureAlbum> Albums { get; set; }
    }
}