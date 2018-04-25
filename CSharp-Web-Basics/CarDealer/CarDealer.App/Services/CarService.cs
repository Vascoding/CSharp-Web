namespace CarDealer.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.App.Models;
    using CarDealer.App.Models.Cars;
    using CarDealer.App.Models.Parts;
    using CarDealer.App.Services.Contracts;
    using CarDealer.Data;
    using CarDealer.Domain;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext context;

        public CarService(CarDealerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CarViewModel> AllByMake(string make)
        {
            using (this.context)
            {
                return this.context.Cars
                    .Where(c => c.Make.ToLower() == make.ToLower())
                    .OrderBy(m => m.Model)
                    .ThenByDescending(d => d.TravelledDistance)
                    .Select(c => new CarViewModel
                    {
                        Model = c.Model,
                        Make = c.Make,
                        TravelledDistance = c.TravelledDistance
                    }).ToList();
            }
        }

        public IEnumerable<CarWithPartsViewModel> AllWithParts()
        {
            using (this.context)
            {
                return this.context.Cars
                    .Select(c => new CarWithPartsViewModel
                    {
                        Model = c.Model,
                        Make = c.Make,
                        TravelledDistance = c.TravelledDistance,
                        Parts = c.Parts.Select(p => new PartViewModel
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price == null ? 0 : p.Part.Price
                        })
                    }).ToList();
            }
        }

        public void Add(string make, string model, long travelledDistance, List<string> partNames)
        {
            using (this.context)
            {
                Car car = new Car
                {
                    Model = model,
                    Make = make,
                    TravelledDistance = travelledDistance
                };
                
                foreach (var partName in partNames)
                {
                    var part = this.context.Parts.FirstOrDefault(p => p.Name == partName);
                    if (part != null)
                    {
                        car.Parts.Add(new PartCars
                        {
                            Car = car,
                            Part = part
                        });
                    }
                }
                this.context.Add(car);
                this.context.SaveChanges();
            }
        }
    }
}