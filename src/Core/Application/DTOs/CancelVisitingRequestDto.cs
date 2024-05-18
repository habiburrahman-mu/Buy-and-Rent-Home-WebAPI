using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CancelVisitingRequestDto
{
    public int VisitingRequestId { get; set; }
    [MaxLength(255)]
    public string CancelReason { get; set; } = null!;
}