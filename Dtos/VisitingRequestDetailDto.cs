using BuyAndRentHomeWebAPI.Data.Entities;
using System;

namespace BuyAndRentHomeWebAPI.Dtos
{
    public class VisitingRequestDetailDto
    {
        public int PropertyId { get; set; }
        public DateTime DateOn { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ContactNumber { get; set; }
        public string Status { get; set; }
        public string StatusLabel { get; set; }
        public string Notes { get; set; }
    }
}