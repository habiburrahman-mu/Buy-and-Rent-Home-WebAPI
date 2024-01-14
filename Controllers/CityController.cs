﻿using AutoMapper;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyAndRentHomeWebAPI.Data.Entities;

namespace BuyAndRentHomeWebAPI.Controllers
{
    //[Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get api/city
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {

            var cities = await _unitOfWork.CityRepository.GetCitiesAsync();

            var citiesDto = _mapper.Map<IEnumerable<CityDto>>(cities);

            return Ok(citiesDto);
        }

        [HttpGet("list/{countryId}")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> GetCityListByCountry(int countryId)
        {
            var cityList = await _unitOfWork.CityRepository.GetCitiesByCountryAsync(countryId);
            var cityDto = _mapper.Map<IEnumerable<CityDto>>(cityList);
            return Ok(cityDto);

        }

        //Post api/city/
        // give city data in body
        [HttpPost]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            city.LastUpdatedOn = DateTime.UtcNow;
            city.LastUpdatedBy = 1;
            _unitOfWork.CityRepository.AddCity(city);
            await _unitOfWork.SaveAsync();
            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
                return BadRequest("Update not allowed");

            var cityFromDb = await _unitOfWork.CityRepository.FindCity(id);

            if (cityFromDb == null)
                return BadRequest("Update not allowed");

            cityFromDb.LastUpdatedOn = DateTime.UtcNow;
            cityFromDb.LastUpdatedBy = 1;
            _mapper.Map(cityDto, cityFromDb);

            await _unitOfWork.SaveAsync();

            return StatusCode(200);
        }

        [HttpPut("updateCityName/{id}")]
        public async Task<IActionResult> UpdateCityName(int id, CityUpdateDto cityUpdateDto)
        {
            var cityFromDb = await _unitOfWork.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedOn = DateTime.UtcNow;
            cityFromDb.LastUpdatedBy = 1;
            _mapper.Map(cityUpdateDto, cityFromDb);
            await _unitOfWork.SaveAsync();

            return StatusCode(200);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var cityFromDb = await _unitOfWork.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedOn = DateTime.UtcNow;
            cityFromDb.LastUpdatedBy = 1;

            cityToPatch.ApplyTo(cityFromDb, ModelState);
            await _unitOfWork.SaveAsync();

            return StatusCode(200);
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
