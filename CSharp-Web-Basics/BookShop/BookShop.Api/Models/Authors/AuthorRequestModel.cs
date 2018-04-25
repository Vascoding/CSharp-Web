using System.ComponentModel.DataAnnotations;

namespace BookShop.Api.Models.Authors
{
    public class AuthorRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}
