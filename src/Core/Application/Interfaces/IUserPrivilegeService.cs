using Application.DTOs;

namespace Application.Interfaces;

public interface IUserPrivilegeService
{
    Task<bool> SaveUserPrivilege(UserPrivilegeSaveDto userPrivilegeSaveDto);
}
