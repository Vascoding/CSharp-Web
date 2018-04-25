namespace CarDealer.App.Controllers
{
    using CarDealer.Domain;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            this.ViewData["anonymous"] = "none";
        }

        protected User Profile { get; set; }
    }
}