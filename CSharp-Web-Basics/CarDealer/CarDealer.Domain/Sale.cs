namespace CarDealer.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int Car_Id { get; set; }

        public virtual Car Car { get; set; }

        [ForeignKey("Customer")]
        public int Customer_Id { get; set; }

        public virtual Customer Customer { get; set; }

        public double Discount { get; set; }
    }
}
