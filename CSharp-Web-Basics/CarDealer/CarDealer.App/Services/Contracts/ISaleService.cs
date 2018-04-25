namespace CarDealer.App.Services.Contracts
{
    using System.Collections.Generic;
    using CarDealer.App.Models.Sales;

    public interface ISaleService
    {
        IEnumerable<SaleViewModel> All();

        AddSaleViewModel GetCustomersAndCars();

        FinalizeViewModel Finalize(FinalizeViewModel model);

        void Add(int customerId, int carId, double discount);
    }
}