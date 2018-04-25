namespace SocialNetwork.Models.Models
{
    public class Friend
    {
        public int MainUserId { get; set; }

        public User MainUser { get; set; }

        public int FriendUserId { get; set; }

        public User FriendUser { get; set; }
    }
}