using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CountryController(ICountryService countryService) : BaseController
    {
        private readonly ICountryService countryService = countryService;

        [HttpGet("list")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetCountryList()
        {
            var countryList = await countryService.GetCountryList();
            return Ok(countryList);
        }
    }
}
