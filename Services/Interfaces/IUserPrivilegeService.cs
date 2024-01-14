using BuyAndRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services.Interfaces
{
    public interface IUserPrivilegeService
    {
        Task<bool> SaveUserPrivilege(UserPrivilegeSaveDto userPrivilegeSaveDto);
    }
}
