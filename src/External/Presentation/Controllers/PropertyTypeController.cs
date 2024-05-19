using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class PropertyTypeController(IPropertyTypeService propertyTypeService) : BaseController
{
    private readonly IPropertyTypeService propertyTypeService = propertyTypeService;

    [HttpGet("list")]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetPropertyType()
    {
        var propertyTypes = await propertyTypeService.GetPropertyTypeList();
        return Ok(propertyTypes);
    }
}
