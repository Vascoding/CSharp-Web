namespace Shop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public Customer()
        {
            this.Orders = new List<Order>();
            this.Reviews = new List<Review>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int SalesmanId { get; set; }

        public Salesman Salesman { get; set; }

        public List<Order> Orders { get; set; }

        public List<Review> Reviews { get; set; }
    }
}