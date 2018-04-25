using BookShop.Api.Infrastructure.Extensions;
using BookShop.Api.Models.Authors;
using BookShop.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        => this.OkOrNotFound(this.authors.Details(id));

        [HttpPost]
        public IActionResult Post([FromBody]AuthorRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = this.authors.Create(model.FirstName, model.LastName);

            return Ok(id);
        }
    }
}
