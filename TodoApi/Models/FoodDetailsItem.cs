using System;
namespace TodoApi.Models
{
    public class FoodDetailsItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string FoodNotes { get; set; }
    }
}
