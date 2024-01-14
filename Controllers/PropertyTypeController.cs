using AutoMapper;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Controllers
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
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetPropertyType()
        {
            var propertyTypes = await _unitOfWork.PropertyTypeRepository.GetPropertyTypesAsync();
            var propertyTypeDto = _mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(propertyTypeDto);
        }
    }
}
