namespace Cats.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cat
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}