namespace CarDealer.App.Services.Contracts
{
    using System.Collections.Generic;
    using CarDealer.App.Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierViewModel> All(string filter);

        IEnumerable<SupplierViewModel> All();
    }
}