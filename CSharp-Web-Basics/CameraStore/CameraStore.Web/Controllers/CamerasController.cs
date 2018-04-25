using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CameraStore.Data.Models.CameraViewModels;
using CameraStore.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using CameraStore.Data.Models;
using CameraStore.Web.Filters;

namespace CameraStore.Web.Controllers
{
    public class CamerasController : Controller
    {
        private const string CameraNotFound = "Camera with id {0} was not found";
        private const string Restricted = "Sorry you are restricted to add new cameras.";

        private readonly ICameraService cameras;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public CamerasController(ICameraService cameras, UserManager<User> userManager, IUserService users)
        {
            this.cameras = cameras;
            this.userManager = userManager;
            this.users = users;
        }

        public IActionResult All()
        {
            var model = this.cameras.All();

            return this.View(model);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (this.users.IsRestricted(this.User.Identity.Name))
            {
                this.TempData["error"] = Restricted;
                return this.RedirectToAction(nameof(All));
            }

            return this.View();
        }

        [Authorize]
        [HttpPost]
        [Log]
        public IActionResult Add(AddCameraModel cameraModel)
        {
            if (this.users.IsRestricted(this.User.Identity.Name))
            {
                this.TempData["error"] = Restricted;
                return this.RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            var userId = this.userManager.GetUserId(User);

            this.cameras.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinIso,
                cameraModel.MaxIso,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMatering,
                cameraModel.Description,
                cameraModel.ImageUrl,
                userId);

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var cameraModel = this.cameras.Details(id);

            return this.View(cameraModel);
        }
        
        public IActionResult Edit(int id)
        {
            var cameraModel = this.cameras.Find(id);
            return this.View(cameraModel);
        }

        [HttpPost]
        [Log]
        [Authorize]
        public IActionResult Edit(int id, AddCameraModel cameraModel)
        {
            var edit = this.cameras.Edit(id, cameraModel); 
            if (!edit)
            {
                this.TempData["error"] = string.Format(CameraNotFound, id);
                return this.RedirectToAction(nameof(Edit));
            }
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Edit));
            }

            return this.RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var cameraModel = this.cameras.Find(id);

            return this.View(cameraModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Confirm(int id)
        {
            this.cameras.Delete(id);

            return this.RedirectToAction(nameof(All));
        }
    }
}
