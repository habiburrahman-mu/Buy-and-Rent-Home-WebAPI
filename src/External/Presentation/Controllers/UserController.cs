using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers;

public class UserController : BaseController
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [AllowAnonymous]
    [HttpGet("PaginatedList")]
    public async Task<IActionResult> GetUserPaginatedList([FromQuery] PaginationParameter paginationParameter)
    {
        var paginatedList = await userService.GetUserPaginatedList(paginationParameter);
        return Ok(paginatedList);
    }
}
