namespace CarDealer.App.Controllers
{
    using CarDealer.App.Models.Cars;
    using CarDealer.App.Common;
    using CarDealer.App.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    public class CarsController : BaseController
    {
        private const string Error = "All fields are required";
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(ICarService cars, IPartService parts)
        {
            this.cars = cars;
            this.parts = parts;
        }

        [Route("cars/{make}")]
        public IActionResult AllByMake(string make)
        {
            var model = this.cars.AllByMake(make);

            return this.View(model);
        }

        [Route("cars/parts")]
        public IActionResult AllWithParts()
        {
            var model = this.cars.AllWithParts();
            return this.View(model);
        }


        [Route("cars/add")]
        public IActionResult Add()
        {
            if (!UserStore.IsAuthenticated())
            {
                return this.RedirectToAction("register", "users");
            }
            var model = this.parts.All();
            return this.View(model);
        }

        [HttpPost]
        [Route("cars/add")]
        public IActionResult Add(AddCarViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["error"] = Error;
                return this.View();
            }
            
            this.cars.Add(model.Make, model.CarModel, model.TravelledDistance, model.PartNames);

            return this.RedirectToAction(model.Make);
        }
    }
}