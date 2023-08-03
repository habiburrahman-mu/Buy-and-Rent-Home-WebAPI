using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class PropertyDetailDto : PropertyListDto
    {
        public int SellRent { get; set; }
        public int PropertyTypeId { get; set; }
        public int FurnishingTypeId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string StreetAddress { get; set; }
        public int TotalFloor { get; set; }
        public int Floor { get; set; }
        public string Landmark { get; set; }
        public double CityLattitude { get; set; }
        public double CityLongitude { get; set; }
        public int? OtherCost { get; set; }
        public bool Gym { get; set; }
        public bool Parking { get; set; }
        public bool SwimmingPool { get; set; }
        public string Description { get; set; }
        public string AvailableDays { get; set; }
        public TimeSpan AvailableStartTime { get; set; }
        public TimeSpan AvailableEndTime { get; set; }
        public int PostedBy { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}