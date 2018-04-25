namespace SocialNetwork.Models.Models
{
    public class PictureAlbum
    {
        public int PictureId { get; set; }

        public Picture Picture { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}