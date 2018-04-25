using CameraStore.Data;
using CameraStore.Data.Models.AdminViewModels;
using CameraStore.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CameraStore.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly CameraDbContext db;

        public AdminService(CameraDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UsersModel> All()
        {
            using (this.db)
            {
                return this.db.Users
                    .Select(u => new UsersModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        IsRestricted = u.IsRestricted
                    })
                    .ToList();
            }
        }

        public UsersModel Find(string id)
        {
            using (this.db)
            {
                return this.db
                    .Users
                    .Where(u => u.Id == id)
                    .Select(u => new UsersModel
                    {
                        Id = u.Id,
                        Email = u.Email
                    })
                    .FirstOrDefault();
            }
        }

        public bool Restrict(string id)
        {
            using (this.db)
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return false;
                }

                user.IsRestricted = true;
                this.db.SaveChanges();

                return true;
            }
        }

        public bool Destrict(string id)
        {
            using (this.db)
            {
                var user = this.db.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return false;
                }

                user.IsRestricted = false;
                this.db.SaveChanges();

                return true;
            }
        }
    }
}
