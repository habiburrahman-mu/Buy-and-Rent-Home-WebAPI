using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class FurnishingType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime LastUpdatedOn { get; set; }

    public int LastUpdatedBy { get; set; }

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
