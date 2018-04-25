using CarDealer.App.Models.Account;
using CarDealer.App.Services.Contracts;
using CarDealer.Data;
using CarDealer.Domain;
using System.Linq;

namespace CarDealer.App.Services
{
    public class UserService : IUserService
    {
        private readonly CarDealerDbContext context;

        public UserService(CarDealerDbContext context)
        {
            this.context = context;
        }

        public User Register(string userName, string email, string password)
        {
            using (this.context)
            {
                User user = new User
                {
                    Email = email,
                    Username = userName,
                    Password = password
                };

                this.context.Users.Add(user);
                this.context.SaveChanges();
                return user;
            }
        }

        public User Exists(string email, string password)
        {
            using (this.context)
            {
                return this.context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            }
        }
    }
}
