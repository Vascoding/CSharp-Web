namespace CarDealer.Domain
{
    using System.Collections.Generic;

    public class Car
    {
        public Car()
        {
            this.Parts = new List<PartCars>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<PartCars> Parts { get; set; }
    }
}
