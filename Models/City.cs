using System;
using System.ComponentModel.DataAnnotations;

namespace BuyandRentHomeWebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime LastUpdated { get; set; }
        public int LastUpdateBy { get; set; }
    }
}
