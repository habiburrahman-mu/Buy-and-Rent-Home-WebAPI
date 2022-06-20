using BuyandRentHomeWebAPI.Data.Repo;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        // Get api/city
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _cityRepository.GetCitiesAsync();
            return Ok(cities);
        }

        //Post api/city/
        // give city data in body
        [HttpPost]
        public async Task<IActionResult> AddCity(City city)
        {
            _cityRepository.AddCity(city);
            await _cityRepository.SaveAsync();
            return StatusCode(201);
        }

        //Delete api/city/{id}
        // delete city data in body
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _cityRepository.DeleteCity(id);
            await _cityRepository.SaveAsync();
            return Ok(id);
        }

    }
}
