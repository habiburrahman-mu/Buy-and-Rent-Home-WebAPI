using BuyandRentHomeWebAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        [HttpGet]
        public IActionResult Get()
        {
            var cities = _dataContext.Cities.ToList();
            return Ok(cities);
        }
    }
}
