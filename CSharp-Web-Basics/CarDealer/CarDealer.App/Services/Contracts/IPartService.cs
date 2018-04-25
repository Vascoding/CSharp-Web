namespace CarDealer.App.Services.Contracts
{
    using System.Collections.Generic;
    using CarDealer.App.Models.Parts;

    public interface IPartService
    {
        void Add(string name, double? price, int quantity, int supplierId);

        IEnumerable<PartViewModel> All();

        PartViewModel Find(int id);

        void Delete(int id);

        EditPartViewModel PartToEdit(int id);

        void Edit(EditPartViewModel model);
    }
}