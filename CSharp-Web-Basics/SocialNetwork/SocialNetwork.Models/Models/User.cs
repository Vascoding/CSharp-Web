namespace SocialNetwork.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.MainUserFriends = new List<Friend>();
            this.Friends = new List<Friend>();
            this.Albums = new List<Album>();
            this.SharedAlbums = new List<UserAlbum>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<Friend> MainUserFriends { get; set; }

        public List<Friend> Friends { get; set; }

        public List<Album> Albums { get; set; }

        public List<UserAlbum> SharedAlbums { get; set; }
    }
}