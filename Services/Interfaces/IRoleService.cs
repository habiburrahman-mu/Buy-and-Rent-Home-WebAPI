using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IRoleService
    {
        Task<PageResult<Role>> GetRolePaginatedList(PaginationParameter paginationParameter);
    }
}
