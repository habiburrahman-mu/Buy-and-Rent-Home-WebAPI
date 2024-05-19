namespace Application.DTOs;

public class DayAvailability
{
    public DateOnly Date { get; set; }
    public string Day {  get; set; }
    public List<TimeSlot> AvailableTimeSlots { get; set; }
}
