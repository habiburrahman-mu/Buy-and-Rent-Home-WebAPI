using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.Data;

namespace Application.Services;

public class FurnishingTypeService(IUnitOfWork unitOfWork, IMapper mapper) : IFurnishingTypeService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<IEnumerable<KeyValuePairDto>> GetFurnishingTypeList()
    {
        var furnishiningType = await unitOfWork.FurnishingTypeRepository.GetFurnishingTypesAsync();
        var furnishiningTypeDto = mapper.Map<IEnumerable<KeyValuePairDto>>(furnishiningType);
        return furnishiningTypeDto;
    }
}
