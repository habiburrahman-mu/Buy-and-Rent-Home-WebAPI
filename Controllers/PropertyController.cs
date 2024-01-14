using BuyAndRentHomeWebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BuyAndRentHomeWebAPI.Services.Interfaces;

namespace BuyAndRentHomeWebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
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
        [HttpGet("myProperty")]
        [Authorize]
        public async Task<IActionResult> GetMyProperty()
        {
            var propertyListDto = await _propertyService.GetMyPropertyList();
            return Ok(propertyListDto);
        }
        
        [HttpGet("myPropertyPaginatedList")]
        [Authorize]
        public async Task<IActionResult> GetMyPropertyPaginatedList([FromQuery] PaginationParameter paginationParameter)
        {
            var paginatedPropertyList = await _propertyService.GetMyPropertyPaginatedList(paginationParameter);
            return Ok(paginatedPropertyList);
        }

        // property/addNew
        [HttpPost("addNew")]
        [Authorize]
        public async Task<IActionResult> AddNewProperty([FromBody] PropertyCreateUpdateDto propertyCreateUpdateDto)
        {
            var propertyId = await _propertyService.AddNewProperty(propertyCreateUpdateDto);
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


    }
}
