using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services;
using BuyandRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("PaginatedList")]
        public async Task<IActionResult> GetRolePaginatedList([FromQuery] PaginationParameter paginationParameter)
        {
            var paginatedList = await userService.GetUserPaginatedList(paginationParameter);
            return Ok(paginatedList);
        }
    }
}
