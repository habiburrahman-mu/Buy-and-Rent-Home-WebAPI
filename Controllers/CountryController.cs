using AutoMapper;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Controllers
{
    public class CountryController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CountryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("list")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetCountries()
        {
            var countryList = await unitOfWork.CountryRepository.GetCountriesAsync();
            var countryDtoList = mapper.Map<IEnumerable<CountryDto>>(countryList);
            return Ok(countryDtoList);
        }
    }
}
