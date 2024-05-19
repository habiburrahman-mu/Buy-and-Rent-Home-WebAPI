using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class FurnishingTypeController(IFurnishingTypeService furnishingTypeService) : BaseController
{
    private readonly IFurnishingTypeService furnishingTypeService = furnishingTypeService;

    [HttpGet("list")]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetFurnishingTypeList()
    {
        var furnishiningType = await furnishingTypeService.GetFurnishingTypeList();
        return Ok(furnishiningType);
    }
}
