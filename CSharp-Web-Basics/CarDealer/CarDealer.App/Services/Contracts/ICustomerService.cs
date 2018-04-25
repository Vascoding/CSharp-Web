namespace CarDealer.App.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using CarDealer.App.Models;
    using CarDealer.App.Models.Customers;
    using CarDealer.Data;

    public interface ICustomerService
    {
        List<ListCustomersViewModel> All(string orderType);

        CustomerViewModel Find(int id);

        void Add(string name, DateTime birthdate);

        AddCustomerViewModel CustomerToEdit(int id);

        bool Edit(int id, string name, DateTime birthDate);

        bool Exists(int id);
    }
}