namespace CarDealer.App.Models.Cars
{
    using System.ComponentModel.DataAnnotations;

    public class CarViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public long TravelledDistance { get; set; }
    }
}