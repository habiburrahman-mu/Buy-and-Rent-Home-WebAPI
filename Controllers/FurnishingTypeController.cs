using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class FurnishingTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FurnishingTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("list")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetFurnishingType()
        {
            var furnishiningType = await _unitOfWork.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furnishiningTypeDto = _mapper.Map<IEnumerable<KeyValuePairDto>>(furnishiningType);
            return Ok(furnishiningTypeDto);
        }
    }
}
