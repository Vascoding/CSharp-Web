using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Api.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound(this Controller controller, object model)
        {
            if (model == null)
            {
                return controller.NotFound();
            }

            return controller.Ok(model);
        }
    }
}
