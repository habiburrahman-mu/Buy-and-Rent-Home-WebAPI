using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities;

public partial class VisitingRequest
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int TakenBy { get; set; }

    public DateTime DateOn { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ContactNumber { get; set; }

    /// <summary>
    /// P: Pending; A: Approved; N: Not Approved
    /// </summary>
    public string Status { get; set; }

    public string Notes { get; set; }

    public virtual Property Property { get; set; }

    public virtual User TakenByNavigation { get; set; }
}
