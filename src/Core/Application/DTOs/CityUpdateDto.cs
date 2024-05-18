using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CityUpdateDto
{
    [Required(ErrorMessage = "Name is mandatory field")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "String length must be between 2-50")]
    [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only numerics are not allowed")]
    public string Name { get; set; }
}
