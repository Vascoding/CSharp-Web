using CameraStore.Service.Contracts;
using CameraStore.Data.Models.UserViewModels;
using CameraStore.Data;
using System.Linq;
using CameraStore.Data.Models.CameraViewModels;

namespace CameraStore.Service.Implementations
{
    public class UserService : IUserService
    {
        readonly CameraDbContext db;

        public UserService(CameraDbContext db)
        {
            this.db = db;
        }

        public UserDetailsModel Details(string sellerId, string loggedUser)
        {
            using (this.db)
            {
                bool isSeller = sellerId == loggedUser;
                return this.db.Users
                    .Where(u => u.Id == sellerId)
                    .Select(u => new UserDetailsModel
                    {
                        Email = u.Email,
                        Phone = u.PhoneNumber,
                        InStock = u.Cameras.Where(c => c.Quantity > 0).Count(),
                        OutOfStock = u.Cameras.Where(c => c.Quantity == 0).Count(),
                        IsSeller = isSeller,
                        Cameras = u.Cameras.Select(c => new CameraModel
                        {
                            Id = c.Id,
                            ImageUrl = c.ImageUrl,
                            Make = c.Make.ToString(),
                            Model = c.Model,
                            Price = c.Price,
                            Quantity = c.Quantity
                        }).ToList()
                    }).FirstOrDefault();
            }
        }

        public bool IsRestricted(string email)
        {
            using (this.db)
            {
                return this.db.Users.FirstOrDefault(u => u.Email == email).IsRestricted;
            }
        }
    }
}
