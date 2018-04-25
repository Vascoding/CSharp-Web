namespace Shop.Models
{
    using System.Collections.Generic;

    public class Order
    {
        public Order()
        {
            this.Items = new List<ItemOrder>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ItemOrder> Items { get; set; }
    }
}