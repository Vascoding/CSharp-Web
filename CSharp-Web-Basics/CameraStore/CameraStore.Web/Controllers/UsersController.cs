using CameraStore.Data.Models;
using CameraStore.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CameraStore.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> usermanager;
        public UsersController(IUserService users, UserManager<User> usermanager)
        {
            this.users = users;
            this.usermanager = usermanager;
        }

        [Route("users/details/{sellerId}")]
        [Authorize]
        public IActionResult Details(string sellerId)
        {
            var loggedUser = this.usermanager.GetUserId(HttpContext.User);
            var userModel = this.users.Details(sellerId, loggedUser);
            if (userModel == null)
            {
                return this.RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return this.View(userModel);
        }
    }
}
