using BuyAndRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System;

namespace BuyAndRentHomeWebAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<UserPrivilegeDto> UserPrivileges { get; set; }
    }
}
