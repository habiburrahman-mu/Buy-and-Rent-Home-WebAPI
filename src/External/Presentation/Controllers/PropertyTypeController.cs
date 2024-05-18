//using AutoMapper;
//using Application.DTOs;
//using Microsoft.AspNetCore.Mvc;

//namespace Presentation.Controllers;

//public class PropertyTypeController : BaseController
//{
//    private readonly IUnitOfWork _unitOfWork;
//    private readonly IMapper _mapper;

//    public PropertyTypeController(IUnitOfWork unitOfWork, IMapper mapper)
//    {
//        _unitOfWork = unitOfWork;
//        _mapper = mapper;
//    }

//    [HttpGet("list")]
//    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
//    public async Task<IActionResult> GetPropertyType()
//    {
//        var propertyTypes = await _unitOfWork.PropertyTypeRepository.GetPropertyTypesAsync();
//        var propertyTypeDto = _mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
//        return Ok(propertyTypeDto);
//    }
//}
