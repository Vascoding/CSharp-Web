using CameraStore.Data.Models.CameraViewModels;
using System.Collections.Generic;

namespace CameraStore.Data.Models.UserViewModels
{
    public class UserDetailsModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public int InStock { get; set; }

        public int OutOfStock { get; set; }

        public bool IsSeller { get; set; }

        public List<CameraModel> Cameras { get; set; }
    }
}
