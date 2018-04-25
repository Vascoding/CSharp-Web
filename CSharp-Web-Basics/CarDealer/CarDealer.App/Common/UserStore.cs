using CarDealer.Domain;

namespace CarDealer.App.Common
{
    public static class UserStore
    {
        public static User Profile { get; set; }

        public static bool IsAuthenticated()
        {
            return Profile != null;
        }
    }
}
