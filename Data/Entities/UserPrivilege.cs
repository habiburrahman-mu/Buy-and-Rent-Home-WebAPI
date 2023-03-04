using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities
{
    public partial class UserPrivilege
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
