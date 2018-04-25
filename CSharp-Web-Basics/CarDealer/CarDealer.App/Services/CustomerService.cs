namespace CarDealer.App.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.App.Models;
    using CarDealer.App.Models.Customers;
    using CarDealer.App.Services.Contracts;
    using CarDealer.Data;
    using CarDealer.Domain;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext context;

        public CustomerService(CarDealerDbContext context)
        {
            this.context = context;
        }

        public List<ListCustomersViewModel> All(string orderType)
        {
            using (this.context)
            {
                List<ListCustomersViewModel> customers = new List<ListCustomersViewModel>();

                if (orderType == "ascending")
                {
                    customers.AddRange(this.context.Customers
                        .OrderBy(b => b.BirthDate)
                        .Where(b => b.IsYoungDriver == false)
                        .Select(c => new ListCustomersViewModel
                        {
                            Name = c.Name,
                            BirthDate = c.BirthDate,
                            IsYoungDriver = c.IsYoungDriver
                        }));
                    customers.AddRange(this.context.Customers
                        .OrderBy(b => b.BirthDate)
                        .Where(b => b.IsYoungDriver)
                        .Select(c => new ListCustomersViewModel
                        {
                            Name = c.Name,
                            BirthDate = c.BirthDate,
                            IsYoungDriver = c.IsYoungDriver
                        }));
                    return customers.ToList();
                }

                else
                {
                    customers.AddRange(this.context.Customers
                        .OrderByDescending(b => b.BirthDate)
                        .Where(b => b.IsYoungDriver == false)
                        .Select(c => new ListCustomersViewModel
                        {
                            Name = c.Name,
                            BirthDate = c.BirthDate,
                            IsYoungDriver = c.IsYoungDriver
                        }));
                    customers.AddRange(this.context.Customers
                        .OrderByDescending(b => b.BirthDate)
                        .Where(b => b.IsYoungDriver)
                        .Select(c => new ListCustomersViewModel
                        {
                            Name = c.Name,
                            BirthDate = c.BirthDate,
                            IsYoungDriver = c.IsYoungDriver
                        }));
                    return customers.ToList();
                }
            }
        }

        public CustomerViewModel Find(int id)
        {
            using (this.context)
            {
                return this.context.Customers
                    .Where(c => c.Id == id)
                    .Select(c => new CustomerViewModel
                    {
                        Name = c.Name,
                        BouthCars = c.Sales.Count,
                        MoneySpent = c.Sales
                        .Sum(p => p.Car.Parts.Sum(s => s.Part.Price)) - (c.Sales.Sum(p => p.Car.Parts.Sum(s => s.Part.Price)) * c.Sales.Sum(s => s.Discount))
                    }).FirstOrDefault();
            }
        }

        public void Add(string name, DateTime birthdate)
        {
            using (this.context)
            {
                int yearsDiff = (int)DateTime.UtcNow.Subtract(birthdate).TotalDays / 365;

                Customer customer = new Customer
                {
                    Name = name,
                    BirthDate = birthdate,
                    IsYoungDriver = yearsDiff < 27
                };

                this.context.Customers.Add(customer);
                this.context.SaveChanges();
            }
        }

        public AddCustomerViewModel CustomerToEdit(int id)
        {
            using (this.context)
            {
                var customer = this.context.Customers
                    .FirstOrDefault(c => c.Id == id);

                if (customer == null)
                {
                    return new AddCustomerViewModel();
                }

                return new AddCustomerViewModel
                {
                    Name = customer.Name,
                    BirthDate = customer.BirthDate
                };
            }
        }

        public bool Edit(int id, string name, DateTime birthDate)
        {
            using (this.context)
            {
                var customer = this.context.Customers.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    customer.Name = name;
                    customer.BirthDate = birthDate;
                    this.context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public bool Exists(int id)
        {
            using (this.context)
            {
                return this.context.Customers.FirstOrDefault(c => c.Id == id) != null;
            }
        }
    }
}