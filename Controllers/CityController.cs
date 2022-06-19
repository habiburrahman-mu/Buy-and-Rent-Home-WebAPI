using BuyandRentHomeWebAPI.Data;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CityController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        // Get api/city
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _dataContext.Cities.ToListAsync();
            return Ok(cities);
        }

        //Post api/city/add?cityName = Miami
        [HttpPost("add")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new City();
            city.Name = cityName;
            await _dataContext.AddAsync(city);
            await _dataContext.SaveChangesAsync();
            return Ok(city);
        }
    }
}
