using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserPrivilege
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
