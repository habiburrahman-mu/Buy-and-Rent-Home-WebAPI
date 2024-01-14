using BuyAndRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;

namespace BuyAndRentHomeWebAPI.Dtos
{
    public class UserPrivilegeSaveDto
    {
        public int UserId { get; set; }
        public List<UserPrivilege> UserPrivilegeList { get; set; } = null!;
    }
}
