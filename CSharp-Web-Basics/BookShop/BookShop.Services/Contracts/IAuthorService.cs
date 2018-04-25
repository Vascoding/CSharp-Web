using BookShop.Services.Models.Authors;

namespace BookShop.Services.Contracts
{
    public interface IAuthorService
    {
        AuthorDetailsServiceModel Details(int id);

        int Create(string firstName, string lastName);
    }
}
