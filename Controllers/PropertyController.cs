using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuyandRentHomeWebAPI.Services.Interfaces;
using System.Linq;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly ISharedService _sharedService;
        private readonly IPropertyService _propertyService;

        public PropertyController(ISharedService sharedService, IPropertyService propertyService)
        {
            _sharedService = sharedService;
            _propertyService = propertyService;
        }

        // property/list/2
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var propertyList = await _propertyService.GetPropertyList(sellRent);
            return Ok(propertyList);
        }

        // property/detail/1
        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var propertyDto = await _propertyService.GetPropertyDetail(id);
            return Ok(propertyDto);
        }

        //[HttpGet("myProperty")]
        //public async Task<IActionResult> GetMyProperty()
        //{
        //    return 
        //}

        // property/addNew/1
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNewProperty([FromBody]PropertyCreateUpdateDto propertyCreateUpdateDto)
        {
            var propertyId = await _propertyService.AddNewProperty(propertyCreateUpdateDto);
            return Ok(propertyId);
        }
    }
}
