using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class UserPrivilegeController : BaseController
{
    private readonly IUserPrivilegeService userPrivilegeService;

    public UserPrivilegeController(IUserPrivilegeService userPrivilegeService)
    {
        this.userPrivilegeService = userPrivilegeService;
    }
    [HttpPost("Save")]
    public Task<bool> SaveUserPrivilege([FromBody] UserPrivilegeSaveDto userPrivilegeSaveDto)
    {
        var result = userPrivilegeService.SaveUserPrivilege(userPrivilegeSaveDto);
        return result;
    }
}
