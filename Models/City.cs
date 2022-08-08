using System;
using System.ComponentModel.DataAnnotations;

namespace BuyandRentHomeWebAPI.Models
{
    public class City : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
