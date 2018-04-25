using CarDealer.App.Models.Cars;
using CarDealer.App.Models.Customers;
using System.Collections.Generic;

namespace CarDealer.App.Models.Sales
{
    public class AddSaleViewModel
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
