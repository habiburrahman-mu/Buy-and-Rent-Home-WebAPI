using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers;

// add authorize
[Authorize]
public class CitiesAreaManagerController : BaseController
{
    private readonly ICitiesAreaManagerService citiesAreaManagerService;
    private readonly IUserContextService userContextService;

    public CitiesAreaManagerController(ICitiesAreaManagerService citiesAreaManagerService, IUserContextService userContextService)
    {
        this.citiesAreaManagerService = citiesAreaManagerService;
        this.userContextService = userContextService;
    }

    [HttpPost("Save")]
    public async Task<IActionResult> SaveCitiesAreaManager([FromBody] CitiesAreaManagerDto citiesAreaManagerDto)
    {
        var result = await citiesAreaManagerService.SaveCitiesAreaManager(citiesAreaManagerDto, userContextService.GetUserId());
        return Ok(result);
    }
}
