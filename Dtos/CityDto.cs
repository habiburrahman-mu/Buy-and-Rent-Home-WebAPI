using System.ComponentModel.DataAnnotations;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Country is mandatory field")]
        [StringLength(50, MinimumLength = 2,  ErrorMessage = "String length must be between 2-50")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Name is mandatory field")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "String length must be between 2-50")]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only numerics are not allowed")]
        public string Name { get; set; }
    }
}
