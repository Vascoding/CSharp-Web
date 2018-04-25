namespace CarDealer.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.App.Models.Cars;
    using CarDealer.App.Models.Customers;
    using CarDealer.App.Models.Sales;
    using CarDealer.App.Services.Contracts;
    using CarDealer.Data;
    using CarDealer.Domain;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext context;

        public SaleService(CarDealerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<SaleViewModel> All()
        {
            using (this.context)
            {
                return this.context.Sales
                    .Select(s => new SaleViewModel
                    {
                        Discount = s.Discount,
                        Car = new CarViewModel
                        {
                            Model = s.Car.Model,
                            Make = s.Car.Make,
                            TravelledDistance = s.Car.TravelledDistance
                        },
                        Customer = new CustomerViewModel
                        {
                            Name = s.Customer.Name
                        },
                        PriceWithDiscount = s.Car.Parts.Sum(p => p.Part.Price) - s.Car.Parts.Sum(p => p.Part.Price) * s.Discount,
                        PriceWithoutDiscount = s.Car.Parts.Sum(p => p.Part.Price)
                    }).ToList();
            }
        }

        public AddSaleViewModel GetCustomersAndCars()
        {
            using (this.context)
            {
                var customers = this.context.Customers
                    .Select(c => new CustomerViewModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                    }).ToList();

                var cars = this.context.Cars
                    .Select(c => new CarViewModel
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    }).ToList();

                AddSaleViewModel sale = new AddSaleViewModel
                {
                    Cars = cars,
                    Customers = customers
                };
                return sale;
            }
        }

        public FinalizeViewModel Finalize(FinalizeViewModel model)
        {
            using (this.context)
            {
                var car = this.context.Cars
                    .Where(c => c.Id == model.CarId)
                    .Select(c => new
                    {
                        car = c.Make + " " + c.Model,
                        carPrice = c.Parts.Sum(s => s.Part.Price) == null ? 0 : c.Parts.Sum(s => s.Part.Price)
                    }).FirstOrDefault();

                var customer = this.context.Customers
                    .FirstOrDefault(c => c.Id == model.CustomerId);

                model.Car = car.car;
                model.CustomerName = customer.Name;
                model.PriceWithDiscount = car.carPrice - car.carPrice * model.Discount;
                model.PriceWithoutDiscount = car.carPrice;

                return model;
            }
        }

        public void Add(int customerId, int carId, double discount)
        {
            using (this.context)
            {
                Sale sale = new Sale
                {
                    Car_Id = carId,
                    Customer_Id = customerId,
                    Discount = discount
                };

                this.context.Sales.Add(sale);
                this.context.SaveChanges();
            }
        }
    }
}