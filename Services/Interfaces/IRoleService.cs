using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetRoleList();
    }
}
