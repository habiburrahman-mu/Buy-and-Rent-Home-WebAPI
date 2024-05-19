namespace Application.DTOs;

public class VisitingRequestCreateDto
{
    public int PropertyId { get; set; }
    public DateOnly DateOn { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string ContactNumber { get; set; }
}