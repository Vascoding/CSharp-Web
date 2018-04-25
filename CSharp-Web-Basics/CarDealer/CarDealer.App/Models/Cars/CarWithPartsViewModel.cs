namespace CarDealer.App.Models.Cars
{
    using System.Collections.Generic;
    using CarDealer.App.Models.Parts;

    public class CarWithPartsViewModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual IEnumerable<PartViewModel> Parts { get; set; }
    }
}