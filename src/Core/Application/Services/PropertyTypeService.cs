using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.Data;

namespace Application.Services;

public class PropertyTypeService(IUnitOfWork unitOfWork, IMapper mapper) : IPropertyTypeService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<IEnumerable<KeyValuePairDto>> GetPropertyTypeList()
    {
        var propertyTypes = await unitOfWork.PropertyTypeRepository.GetPropertyTypesAsync();
        var propertyTypeDtoList = mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
        return propertyTypeDtoList;
    }
}
