using CameraStore.Data.Models.AdminViewModels;
using System.Collections.Generic;

namespace CameraStore.Service.Contracts
{
    public interface IAdminService
    {
        IEnumerable<UsersModel> All();

        UsersModel Find(string id);

        bool Restrict(string id);

        bool Destrict(string id);
    }
}
