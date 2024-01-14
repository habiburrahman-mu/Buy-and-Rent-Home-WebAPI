using System;
using System.Collections.Generic;

namespace BuyAndRentHomeWebAPI.Data.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsLocked { get; set; }

    public string Description { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; }

    public virtual User UpdatedByNavigation { get; set; }

    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();
}
