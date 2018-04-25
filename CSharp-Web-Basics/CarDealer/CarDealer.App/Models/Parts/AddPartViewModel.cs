namespace CarDealer.App.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class AddPartViewModel
    {
        [Required]
        public string Name { get; set; }

        public double? Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public int SupplierId { get; set; }
    }
}