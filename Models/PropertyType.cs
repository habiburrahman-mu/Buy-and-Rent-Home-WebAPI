using System.ComponentModel.DataAnnotations;

namespace BuyandRentHomeWebAPI.Models
{
    public class PropertyType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}