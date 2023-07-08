using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities;

public partial class CitiesAreaManager
{
    public int Id { get; set; }

    public int ManagerId { get; set; }

    public int CityId { get; set; }

    public int LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public virtual City City { get; set; }

    public virtual User LastUpdatedByNavigation { get; set; }

    public virtual User Manager { get; set; }
}
