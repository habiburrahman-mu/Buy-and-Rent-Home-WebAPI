using System;

namespace BuyAndRentHomeWebAPI.Dtos
{
    public class TimeSlot
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
