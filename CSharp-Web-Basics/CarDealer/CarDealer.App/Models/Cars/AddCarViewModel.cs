namespace CarDealer.App.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCarViewModel
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public long TravelledDistance { get; set; }

        public List<string> PartNames { get; set; }
    }
}