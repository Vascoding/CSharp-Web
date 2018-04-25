using System.ComponentModel.DataAnnotations;
using CameraStore.Data.Enumerations;

namespace CameraStore.Data.Models
{
    public class Camera
    {
        public int Id { get; set; }

        [Required]
        public CameraMake Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        [RegularExpression("50|100")]
        public int MinIso { get; set; }

        [Range(200, 409600)]
        public int MaxIso { get; set; }

        public bool IsFullFrame { get; set; }

        [MaxLength(15)]
        public string VideoResolution { get; set; }

        [MaxLength(6000)]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }

        public LightMatering LightMatering { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
