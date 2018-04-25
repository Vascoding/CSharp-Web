namespace CarDealer.App.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddCustomerViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}