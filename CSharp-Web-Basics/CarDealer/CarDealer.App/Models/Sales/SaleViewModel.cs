namespace CarDealer.App.Models.Sales
{
    using CarDealer.App.Models.Cars;
    using CarDealer.App.Models.Customers;

    public class SaleViewModel
    {
        public double Discount { get; set; }

        public CarViewModel Car { get; set; }

        public CustomerViewModel Customer { get; set; }

        public double? PriceWithDiscount { get; set; }

        public double? PriceWithoutDiscount { get; set; }
    }
}