using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Property
{
    public int Id { get; set; }

    public int SellRent { get; set; }

    public string Name { get; set; } = null!;

    public int PropertyTypeId { get; set; }

    public int FurnishingTypeId { get; set; }

    public int Bedroom { get; set; }

    public int? Bathroom { get; set; }

    public int? CommonSpace { get; set; }

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public string StreetAddress { get; set; } = null!;

    public int TotalFloor { get; set; }

    public int Floor { get; set; }

    public int Area { get; set; }

    public int RentPrice { get; set; }

    public int? OtherCost { get; set; }

    public bool Gym { get; set; }

    public bool Parking { get; set; }

    public bool SwimmingPool { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? Description { get; set; }

    public string AvailableDays { get; set; } = null!;

    public TimeOnly AvailableStartTime { get; set; }

    public TimeOnly AvailableEndTime { get; set; }

    public DateTime PostedOn { get; set; }

    public int PostedBy { get; set; }

    public DateTime LastUpdatedOn { get; set; }

    public int LastUpdatedBy { get; set; }

    /// <summary>
    /// A: Active,
    /// D: Draft,
    /// C: Complete
    /// </summary>
    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual FurnishingType FurnishingType { get; set; } = null!;

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual User PostedByNavigation { get; set; } = null!;

    public virtual PropertyType PropertyType { get; set; } = null!;

    public virtual ICollection<VisitingRequest> VisitingRequests { get; set; } = new List<VisitingRequest>();
}
