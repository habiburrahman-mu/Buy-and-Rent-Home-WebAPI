using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Interfaces;
using Presentation.Services;
using Presentation.ViewModels;

namespace Presentation.Controllers;

public class PropertyController : BaseController
{
    private readonly IPropertyService _propertyService;
    private readonly IUserContextService userContextService;

    public PropertyController(IPropertyService propertyService, IUserContextService userContextService)
    {
        _propertyService = propertyService;
        this.userContextService = userContextService;
    }

    // property/list/2
    [HttpGet("propertyPaginatedList/{sellRent}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPropertyList(int sellRent, [FromQuery] PaginationParameter paginationParameter)
    {
        var paginatedPropertyList = await _propertyService.GetPropertyPaginatedList(paginationParameter, sellRent);
        return Ok(paginatedPropertyList);
    }

    // property/detail/1
    [HttpGet("detail/{id}")]
    public async Task<IActionResult> GetPropertyDetail(int id)
    {
        var propertyDto = await _propertyService.GetPropertyDetail(id);
        return Ok(propertyDto);
    }

    // property/myProperty
    [HttpGet("myPropertyList")]
    [Authorize]
    public async Task<IActionResult> GetMyProperty()
    {
        var propertyListDto = await _propertyService.GetMyPropertyList(userContextService.GetUserId());
        return Ok(propertyListDto);
    }

    [HttpGet("myPropertyPaginatedList")]
    [Authorize]
    public async Task<IActionResult> GetMyPropertyPaginatedList([FromQuery] PaginationParameter paginationParameter)
    {
        var paginatedPropertyList = await _propertyService.GetMyPropertyPaginatedList(paginationParameter, userContextService.GetUserId());
        return Ok(paginatedPropertyList);
    }

    // property/addNew
    [HttpPost("save")]
    [Authorize]
    public async Task<IActionResult> SaveProperty([FromBody] PropertyCreateUpdateDto propertyCreateUpdateDto)
    {
        var propertyId = await _propertyService.SaveProperty(propertyCreateUpdateDto, userContextService.GetUserId());
        return Ok(propertyId);
    }

    // property/delete/id
    [HttpDelete("delete/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        var result = await _propertyService.DeleteProperty(id);
        return Ok(result);
    }

    [HttpGet("getAvailableSlotsForNext7Days/{propertyId}")]
    [Authorize]
    public async Task<IActionResult> GetAvailableSlotsForNext7Days(int propertyId)
    {
        var result = await _propertyService.GetAvailableSlotsForNext7Days(propertyId);
        return Ok(result);
    }

    [HttpPut("updatePropertyStatus")]
    public async Task<IActionResult> UpdatePropertyStatus([FromBody] PropertyUpdateStatusDto propertyUpdateStatusDto)
    {
        var result = await _propertyService.UpdatePropertyStatus(propertyUpdateStatusDto.Id, propertyUpdateStatusDto.Status);
        return Ok(true);
    }
}
