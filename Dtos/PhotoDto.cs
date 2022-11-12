using System;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int PropertyId { get; set; }
    }
}
