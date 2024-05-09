using System.ComponentModel.DataAnnotations;

namespace BuyAndRentHomeWebAPI.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? AreaInKm { get; set; }
    }
}
