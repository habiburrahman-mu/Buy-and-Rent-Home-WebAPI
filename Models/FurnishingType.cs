using System.ComponentModel.DataAnnotations;

namespace BuyandRentHomeWebAPI.Models
{
    public class FurnishingType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}