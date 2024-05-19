using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.Data;

namespace Application.Services;

public class CityService(IMapper mapper, IUnitOfWork unitOfWork) : ICityService
{
    private readonly IMapper mapper = mapper;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<List<CityDto>> GetCityListByCountry(int countryId)
    {
        var cityList = await unitOfWork.CityRepository.GetCitiesByCountryAsync(countryId);
        var cityDto = mapper.Map<List<CityDto>>(cityList);
        return cityDto;

    }
}
