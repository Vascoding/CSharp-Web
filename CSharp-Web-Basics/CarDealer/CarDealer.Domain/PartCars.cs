namespace CarDealer.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class PartCars
    {
        [ForeignKey("Part")]
        public int Part_Id { get; set; }

        public Part Part { get; set; }

        [ForeignKey("Car")]
        public int Car_Id { get; set; }

        public Car Car { get; set; }
    }
}