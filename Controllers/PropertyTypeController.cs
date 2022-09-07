using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPropertyType()
        {
            var propertyTypes = await _unitOfWork.PropertyTypeRepository.GetPropertyTypesAsync();
            var propertyTypeDto = _mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(propertyTypeDto);
        }
    }
}
