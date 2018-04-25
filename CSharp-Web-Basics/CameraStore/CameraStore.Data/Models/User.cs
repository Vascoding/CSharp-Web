using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CameraStore.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public List<Camera> Cameras { get; set; } = new List<Camera>();

        public bool IsRestricted { get; set; } = false;
    }
}
