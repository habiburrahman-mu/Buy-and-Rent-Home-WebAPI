using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
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
}
