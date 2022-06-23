using System;

namespace BuyandRentHomeWebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LastUpdateBy { get; set; }
    }
}
