namespace CarDealer.App.Models.Customers
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int BouthCars { get; set; }

        public double? MoneySpent { get; set; }
    }
}