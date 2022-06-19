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

        //Post api/city/
        // give city data in body
        [HttpPost]
        public async Task<IActionResult> AddCity(City city)
        {
            await _dataContext.Cities.AddAsync(city);
            await _dataContext.SaveChangesAsync();
            return Ok(city);
        }

        //Post api/city/{id}
        // give city data in body
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _dataContext.Cities.FindAsync(id);
            _dataContext.Remove(city);
            await _dataContext.SaveChangesAsync();
            return Ok(id);
        }

    }
}
