﻿using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            UserPrivileges = new HashSet<UserPrivilege>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual User UpdatedByNavigation { get; set; }
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }
    }
}