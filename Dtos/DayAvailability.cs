using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class DayAvailability
    {
        public DateTime Date { get; set; }
        public string Day {  get; set; }
        public List<TimeSlot> AvailableTimeSlots { get; set; }
    }
}
