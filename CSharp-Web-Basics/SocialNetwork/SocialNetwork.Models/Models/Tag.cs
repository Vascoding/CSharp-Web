namespace SocialNetwork.Models.Models
{
    using System.Collections.Generic;
    using SocialNetwork.Models.Attributes;

    public class Tag
    {
        public Tag()
        {
            this.Albums = new List<TagAlbum>();
        }

        public int Id { get; set; }

        [Tag]
        public string Name { get; set; }

        public List<TagAlbum> Albums { get; set; }
    }
}