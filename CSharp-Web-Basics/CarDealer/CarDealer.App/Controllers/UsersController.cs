using CarDealer.App.Common;
using CarDealer.App.Models.Account;
using CarDealer.App.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.App.Controllers
{
    public class UsersController : BaseController
    {
        private const string RegisterError = "Email and password are required and password must match confirm password!";
        private const string LoginError = "Invalid email or password!";

        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        public IActionResult Register()
        {
            return this.View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid || model.Password != model.ConfirmPassword)
            {
                this.ViewData["error"] = RegisterError;
                return this.Register();
            }

            var user = this.users.Register(model.Username, model.Email, model.Password);
            UserStore.Profile = user;

            this.ViewData["anonymous"] = "none";
            return this.Redirect("/");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = this.users.Exists(model.Email, model.Password);
            if (user != null)
            {
                UserStore.Profile = user;
                return this.Redirect("/");
            }

            this.ViewData["error"] = LoginError;
            return this.Login();
        }
    }
}