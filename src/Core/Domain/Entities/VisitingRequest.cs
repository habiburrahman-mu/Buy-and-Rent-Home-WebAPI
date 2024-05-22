using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class VisitingRequest
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int TakenBy { get; set; }

    public DateOnly DateOn { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ContactNumber { get; set; } = null!;

    /// <summary>
    /// P: Pending; A: Approved; N: Not Approved
    /// </summary>
    public string Status { get; set; } = null!;

    public bool IsBlocked { get; set; }

    public bool IsActive { get; set; }

    public string? Notes { get; set; }

    public virtual Property Property { get; set; } = null!;

    public virtual User TakenByNavigation { get; set; } = null!;
}
