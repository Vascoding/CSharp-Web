namespace CarDealer.App.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.App.Models.Parts;
    using CarDealer.App.Services.Contracts;
    using CarDealer.Data;
    using CarDealer.Domain;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext context;

        public PartService(CarDealerDbContext context)
        {
            this.context = context;
        }

        public void Add(string name, double? price, int quantity, int supplierId)
        {
            using (this.context)
            {
                Part part = new Part
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Supplier_Id = supplierId
                };

                this.context.Parts.Add(part);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<PartViewModel> All()
        {
            using (this.context)
            {
                return this.context.Parts
                    .Select(p => new PartViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }).ToList();
            }
        }

        public PartViewModel Find(int id)
        {
            using (this.context)
            {
                return this.context.Parts
                    .Where(p => p.Id == id)
                    .Select(p => new PartViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    }).FirstOrDefault();
            }
        }

        public void Delete(int id)
        {
            using (this.context)
            {
                var part = this.context.Parts.FirstOrDefault(p => p.Id == id);

                if (part != null)
                {
                    this.context.Parts.Remove(part);
                    this.context.SaveChanges();
                }
            }
        }

        public EditPartViewModel PartToEdit(int id)
        {
            using (this.context)
            {
                return this.context.Parts
                    .Where(p => p.Id == id)
                    .Select(p => new EditPartViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Quantity = p.Quantity
                    }).FirstOrDefault();
            }
        }

        public void Edit(EditPartViewModel model)
        {
            using (this.context)
            {
                var part = this.context.Parts.FirstOrDefault(p => p.Id == model.Id);
                if (part != null)
                {
                    part.Price = model.Price;
                    part.Quantity = model.Quantity;
                    this.context.SaveChanges();
                }
            }
        }
    }
}