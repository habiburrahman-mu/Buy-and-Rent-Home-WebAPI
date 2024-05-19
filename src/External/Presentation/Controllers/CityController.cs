using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

//[Authorize]
public class CityController(ICityService cityService) : BaseController
{
    private readonly ICityService cityService = cityService;

    [HttpGet("list/{countryId}")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.None)]
    public async Task<IActionResult> GetCityListByCountry(int countryId)
    {
        var cityList = await cityService.GetCityListByCountry(countryId);
        return Ok(cityList);
    }
}
