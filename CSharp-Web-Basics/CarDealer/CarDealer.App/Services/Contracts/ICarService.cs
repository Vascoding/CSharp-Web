namespace CarDealer.App.Services.Contracts
{
    using System.Collections.Generic;
    using CarDealer.App.Models.Cars;

    public interface ICarService
    {
        IEnumerable<CarViewModel> AllByMake(string make);

        IEnumerable<CarWithPartsViewModel> AllWithParts();

        void Add(string make, string model, long travelledDistance, List<string> partNames);
    }
}