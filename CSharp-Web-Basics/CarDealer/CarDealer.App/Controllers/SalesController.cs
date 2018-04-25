namespace CarDealer.App.Controllers
{
    using CarDealer.App.Common;
    using CarDealer.App.Models.Sales;
    using CarDealer.App.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    public class SalesController : BaseController
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("Sales")]
        public IActionResult All()
        {
            var model = this.sales.All();

            return this.View(model);
        }

        public IActionResult Add()
        {
            if (!UserStore.IsAuthenticated())
            {
                return this.RedirectToAction("register", "users");
            }

            var model = this.sales.GetCustomersAndCars();

            return this.View(model);
        }

       
        
        public IActionResult Finalize(FinalizeViewModel sale)
        {
            if (!UserStore.IsAuthenticated())
            {
                return this.RedirectToAction("register", "users");
            }
            var model = this.sales.Finalize(sale);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Confirm(FinalizeViewModel sale)
        {
            if (!UserStore.IsAuthenticated())
            {
                return this.RedirectToAction("register", "users");
            }
            this.sales.Add(sale.CustomerId, sale.CarId, sale.Discount);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}