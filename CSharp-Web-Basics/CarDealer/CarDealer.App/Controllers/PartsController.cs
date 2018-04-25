using Microsoft.AspNetCore.Mvc;

namespace CarDealer.App.Controllers
{
    using CarDealer.App.Models.Parts;
    using CarDealer.App.Services.Contracts;
    using System.Security.Claims;
    public class PartsController : BaseController
    {
        private const string Error = "Name and supplier of the part is required!";

        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        
        public IActionResult Add()
        {
            var model = this.suppliers.All();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(AddPartViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["error"] = Error;

                return this.Add();
            }
            this.SignIn(new ClaimsPrincipal(), model.Name);
            this.parts.Add(model.Name, model.Price, model.Quantity, model.SupplierId);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
        {
            var model = this.parts.All();

            return this.View(model);
        }

        [Route("parts/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var model = this.parts.Find(id);

            return this.View(model);
        }
        
        [Route("parts/confirm/{id}")]
        public IActionResult Confirm(int id)
        {
            this.parts.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            var model = this.parts.PartToEdit(id);

            if (model.Id == 0)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditPartViewModel model)
        {
            this.parts.Edit(model);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}