namespace Application.DTOs;

public class PropertyDetailDto : PropertyListDto
{
    public int SellRent { get; set; }
    public int PropertyTypeId { get; set; }
    public int FurnishingTypeId { get; set; }
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public string StreetAddress { get; set; }
    public int TotalFloor { get; set; }
    public int Floor { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double CityLatitude { get; set; }
    public double CityLongitude { get; set; }
    public int? OtherCost { get; set; }
    public bool Gym { get; set; }
    public bool Parking { get; set; }
    public bool SwimmingPool { get; set; }
    public string Description { get; set; }
    public string AvailableDays { get; set; }
    public TimeOnly AvailableStartTime { get; set; }
    public TimeOnly AvailableEndTime { get; set; }
    public int PostedBy { get; set; }
    /// <summary>
    /// A: Active,
    /// D: Draft,
    /// C: Complete
    /// </summary>
    public string Status { get; set; } = null!;
    public ICollection<PhotoDto> Photos { get; set; }
}