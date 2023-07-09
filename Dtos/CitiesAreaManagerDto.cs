using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class CitiesAreaManagerDto
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }
        public List<CityDto> Cities { get; set; } = new List<CityDto>();
    }
}
