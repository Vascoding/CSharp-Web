namespace CarDealer.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.App.Models.Suppliers;
    using CarDealer.App.Services.Contracts;
    using CarDealer.Data;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext context;

        public SupplierService(CarDealerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<SupplierViewModel> All(string filter)
        {
            using (this.context)
            {
                bool isImoporter = filter.ToLower() != "local";

                return this.context.Suppliers
                    .Where(s => s.IsImporter == isImoporter)
                    .Select(s => new SupplierViewModel
                    {
                        Name = s.Name,
                        Id = s.Id,
                        Parts = s.Parts.Count
                    }).ToList();
            }
        }

        public IEnumerable<SupplierViewModel> All()
        {
            using (this.context)
            {
                return this.context.Suppliers
                    .Select(s => new SupplierViewModel
                    {
                        Name = s.Name,
                        Id = s.Id,
                        Parts = s.Parts.Count
                    }).ToList();
            }
        }
    }
}