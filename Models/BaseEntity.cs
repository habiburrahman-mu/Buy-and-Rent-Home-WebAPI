using System;

namespace BuyandRentHomeWebAPI.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public int LastUpdatedBy { get; set; }
    }
}
