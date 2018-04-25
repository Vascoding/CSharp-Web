namespace CarDealer.App.Controllers
{
    using System.Linq;
    using CarDealer.App.Models.Customers;
    using CarDealer.App.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : BaseController
    {
        private const string CustomerErrorMessage = "Name and birthdate are required";
        private const string AddCustomerSuccessMessage = "Customer added successfully";
        private const string UserNotFoundError = "User with id {0} does not exists!";
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("customers/all/{orderType}")]
        public IActionResult All(string orderType)
        {
            var allCustomers = this.customers.All(orderType).ToList();

            return this.View(allCustomers);
        }

        [Route("customers/{id}")]
        public IActionResult GetCustomer(int id)
        {
            var model = this.customers.Find(id);
            return this.View(model);
        }

        [Route("customers/add")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Route("customers/add")]
        public IActionResult Add(AddCustomerViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["error"] = CustomerErrorMessage;
                return this.View();
            }
            this.ViewData["success"] = AddCustomerSuccessMessage;
            this.customers.Add(model.Name, model.BirthDate);
            return this.View();
        }

        
        public IActionResult Edit(int id)
        {
            var model = this.customers.CustomerToEdit(id);

            if (model.Name == null)
            {
                this.ViewData["error"] = string.Format(UserNotFoundError, id);
                return this.View(model);
            }
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, AddCustomerViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["error"] = CustomerErrorMessage;
                return this.Edit(id);
            }
            
            if (this.customers.Edit(id, model.Name, model.BirthDate))
            {
                return this.RedirectToAction(nameof(this.GetCustomer), id);
            }

            this.ViewData["error"] = CustomerErrorMessage;
            return this.View();
        }
    }
}