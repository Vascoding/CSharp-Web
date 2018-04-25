namespace CarDealer.App.Controllers
{
    using CarDealer.App.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    public class SuppliersController : BaseController
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        [Route("suppliers/{filter}")]
        public IActionResult All(string filter)
        {
            var model = this.suppliers.All(filter);
           
            return this.View(model);
        }
    }
}