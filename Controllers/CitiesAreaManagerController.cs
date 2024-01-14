using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Controllers
{
    // add authorize
    [Authorize]
    public class CitiesAreaManagerController : BaseController
    {
        private readonly ICitiesAreaManagerService citiesAreaManagerService;

        public CitiesAreaManagerController(ICitiesAreaManagerService citiesAreaManagerService)
        {
            this.citiesAreaManagerService = citiesAreaManagerService;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveCitiesAreaManager([FromBody] CitiesAreaManagerDto citiesAreaManagerDto)
        {
            var result = await citiesAreaManagerService.SaveCitiesAreaManager(citiesAreaManagerDto);
            return Ok(result);
        }
    }
}
