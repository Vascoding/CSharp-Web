namespace Shop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public Item()
        {
            this.Orders = new List<ItemOrder>();
            this.Reviews = new List<Review>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<ItemOrder> Orders { get; set; }

        public List<Review> Reviews { get; set; }
    }
}