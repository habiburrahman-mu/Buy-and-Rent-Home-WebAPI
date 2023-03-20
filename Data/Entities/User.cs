using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Countries = new HashSet<Country>();
            Photos = new HashSet<Photo>();
            Properties = new HashSet<Property>();
            RoleCreatedByNavigations = new HashSet<Role>();
            RoleUpdatedByNavigations = new HashSet<Role>();
            UserPrivileges = new HashSet<UserPrivilege>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Role> RoleCreatedByNavigations { get; set; }
        public virtual ICollection<Role> RoleUpdatedByNavigations { get; set; }
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }
    }
}
