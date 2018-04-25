using CameraStore.Data.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CameraStore.Data.Models.CameraViewModels
{
    public class AddCameraModel
    {
        [Required]
        public CameraMake Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Range(1, 30)]
        [Display(Name = "Min Shutter Speed")]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        [Display(Name = "Max Shutter Speed")]
        public int MaxShutterSpeed { get; set; }

        [RegularExpression("50|100", ErrorMessage = "Min ISO must be eather 50 or 100")]
        [Display(Name = "Min Iso")]
        public int MinIso { get; set; }

        [Range(200, 409600)]
        [Display(Name = "Max Iso")]
        public int MaxIso { get; set; }

        [Display(Name = "Full Frame")]
        [Required]
        public bool IsFullFrame { get; set; }

        [StringLength(15, MinimumLength = 2)]
        [Display(Name = "Video Resolutin")]
        public string VideoResolution { get; set; }

        [StringLength(6000)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Light Matering")]
        public IEnumerable<LightMatering> LightMatering { get; set; }
    }
}
