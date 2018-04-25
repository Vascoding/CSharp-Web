﻿namespace CarDealer.App.Models.Parts
{
    
    public class EditPartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public double? Price { get; set; }
        
        public int Quantity { get; set; }
    }
}