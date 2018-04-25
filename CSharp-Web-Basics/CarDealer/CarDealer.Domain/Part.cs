namespace CarDealer.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Part
    {
        public Part()
        {
            this.Cars = new List<PartCars>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<PartCars> Cars { get; set; }

        [ForeignKey("Supplier")]
        public int Supplier_Id { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
