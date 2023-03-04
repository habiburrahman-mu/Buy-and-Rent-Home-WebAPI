using System;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class PropertyListDto
    {
        public int Id { get; set; }
        public int SellRent { get; set; }
        public string Name { get; set; }
        public string PropertyType { get; set; }
        public int Bedroom { get; set; }
        public int? Bathroom { get; set; }
        public int? CommonSpace { get; set; }
        public int Area { get; set; }
        public int RentPrice { get; set; }
        public string FurnishingType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Floor { get; set; }
        public bool ReadyToMove { get; set; }
        public DateTime PostedOn { get; set; }
        public string PrimaryPhoto { get; set; }
    }
}
