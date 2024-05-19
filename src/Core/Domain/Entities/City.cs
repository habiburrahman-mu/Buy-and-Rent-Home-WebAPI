using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double? AreaInKm { get; set; }

    public int CountryId { get; set; }

    public int? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public virtual ICollection<CitiesAreaManager> CitiesAreaManagers { get; set; } = new List<CitiesAreaManager>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
