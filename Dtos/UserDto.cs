using BuyandRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System;

namespace BuyandRentHomeWebAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<UserPrivilegeDto> UserPrivileges { get; set; }
    }
}
