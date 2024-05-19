using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Photo
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsPrimary { get; set; }

    public int PropertyId { get; set; }

    public DateTime LastUpdatedOn { get; set; }

    public int LastUpdatedBy { get; set; }

    public virtual User LastUpdatedByNavigation { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
