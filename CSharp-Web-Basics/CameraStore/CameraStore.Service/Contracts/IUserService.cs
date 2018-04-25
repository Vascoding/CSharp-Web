using CameraStore.Data.Models.UserViewModels;

namespace CameraStore.Service.Contracts
{
    public interface IUserService
    {
        UserDetailsModel Details(string sellerId, string loggedUser);

        bool IsRestricted(string email);
    }
}
