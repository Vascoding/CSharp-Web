using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(70)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Copies { get; set; }

        public int Edition { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public int? AgeRestriction { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();
    }
}
