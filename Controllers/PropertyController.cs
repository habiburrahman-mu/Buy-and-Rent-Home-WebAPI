using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // property/list/2
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties = await _unitOfWork.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDto);
        }

        // property/detail/1
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDto = _mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDto);
        }

        // property/addNew/1
        [HttpPost("AddNew")]
        [AllowAnonymous]
        public async Task<IActionResult> AddNewProperty([FromBody]PropertyCreateUpdateDto propertyCreateUpdateDto)
        {
            var property = _mapper.Map<Property>(propertyCreateUpdateDto);
            property.PostedOn = new DateTime();
            property.PostedBy = 4; // Admin - Todo -> dynamic
            property.LastUpdatedOn = property.PostedOn;
            property.LastUpdatedBy = property.PostedBy;

            await _unitOfWork.PropertyRepository.AddProperty(property);
            await _unitOfWork.SaveAsync();

            return Ok(property.Id);
        }
    }
}
