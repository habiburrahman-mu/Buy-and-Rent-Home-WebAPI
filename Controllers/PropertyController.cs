using BuyandRentHomeWebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BuyandRentHomeWebAPI.Services.Interfaces;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // property/list/2
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var propertyList = await _propertyService.GetPropertyList(sellRent);
            return Ok(propertyList);
        }

        // property/detail/1
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var propertyDto = await _propertyService.GetPropertyDetail(id);
            return Ok(propertyDto);
        }

        // property/myProperty
        [HttpGet("myProperty")]
        public async Task<IActionResult> GetMyProperty()
        {
            var propertyListDto = await _propertyService.GetMyPropertyList();
            return Ok(propertyListDto);
        }

        // property/addNew/1
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNewProperty([FromBody] PropertyCreateUpdateDto propertyCreateUpdateDto)
        {
            var propertyId = await _propertyService.AddNewProperty(propertyCreateUpdateDto);
            return Ok(propertyId);
        }
    }
}
