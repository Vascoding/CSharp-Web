using BookShop.Data;
using BookShop.Services.Contracts;
using BookShop.Services.Models.Authors;
using AutoMapper.QueryableExtensions;
using System.Linq;
using BookShop.Data.Models;

namespace BookShop.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db; 
        }

        public AuthorDetailsServiceModel Details(int id)
        => this.db
            .Authors
            .Where(a => a.Id == id)
            .ProjectTo<AuthorDetailsServiceModel>()
            .FirstOrDefault();

        public int Create(string firstName, string lastName)
        {
            var author = new Author
            {
                FirstName = firstName,
                LastName = lastName
            };

            this.db.Authors.Add(author);
            this.db.SaveChanges();

            return author.Id;
        }
    }
}
