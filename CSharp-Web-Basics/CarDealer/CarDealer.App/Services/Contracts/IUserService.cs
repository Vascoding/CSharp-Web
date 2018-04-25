using CarDealer.App.Models.Account;
using CarDealer.Domain;

namespace CarDealer.App.Services.Contracts
{
    public interface IUserService
    {
        User Register(string userName, string email, string password);

        User Exists(string email, string password);
    }
}
