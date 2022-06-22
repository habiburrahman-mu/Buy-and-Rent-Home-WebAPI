using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // Get api/city
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _unitOfWork.CityRepository.GetCitiesAsync();

            var citiesDto = from city in cities
                            select new CityDto()
                            {
                                Id = city.Id,
                                Name = city.Name
                            };

            return Ok(citiesDto);
        }

        //Post api/city/
        // give city data in body
        [HttpPost]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = new City
            {
                Name = cityDto.Name,
                LastUpdateBy = 1,
                LastUpdated = DateTime.Now
            };
            _unitOfWork.CityRepository.AddCity(city);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        //Delete api/city/{id}
        // delete city data in body
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            _unitOfWork.CityRepository.DeleteCity(id);
            await _unitOfWork.SaveAsync();
            return Ok(id);
        }

    }
}
