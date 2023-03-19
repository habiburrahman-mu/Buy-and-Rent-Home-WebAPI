using BuyandRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IUserPrivilegeService
    {
        Task<bool> SaveUserPrivilege(UserPrivilegeSaveDto userPrivilegeSaveDto);
    }
}
