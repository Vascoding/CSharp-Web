
namespace CarDealer.App.Models.Sales
{
    public class FinalizeViewModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int CarId { get; set; }

        public string Car { get; set; }

        public double Discount { get; set; }

        public double? PriceWithDiscount { get; set; }

        public double? PriceWithoutDiscount { get; set; }
    }
}
