using CameraStore.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CameraStore.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private const string SuccessRestrict = "This user has been restricted successfully.";
        private const string SuccessDestrict = "This user has been destricted successfully.";

        private readonly IAdminService admins;

        public AdminController(IAdminService admins)
        {
            this.admins = admins;
        }

        public IActionResult All()
        {
            var model = this.admins.All();

            return this.View(model);
        }

        public IActionResult Restrict(string id)
        {
            var model = this.admins.Find(id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Confirm(string id)
        {
            var isValid = this.admins.Restrict(id);

            if (!isValid)
            {
                return this.NotFound();
            }

            this.TempData["success"] = SuccessRestrict;
            return this.RedirectToAction(nameof(All));
        }

        public IActionResult Destrict(string id)
        {
            var model = this.admins.Find(id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Allow(string id)
        {
            var isValid = this.admins.Destrict(id);

            if (!isValid)
            {
                return this.NotFound();
            }

            this.TempData["success"] = SuccessDestrict;
            return this.RedirectToAction(nameof(All));
        }
    }
}
