using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces.Data;

namespace Application.Services;

public class CountryService(IUnitOfWork unitOfWork, IMapper mapper) : ICountryService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<IEnumerable<CountryDto>> GetCountryList()
    {
        var countryList = await unitOfWork.CountryRepository.GetCountriesAsync();
        var countryDtoList = mapper.Map<IEnumerable<CountryDto>>(countryList);
        return countryDtoList;
    }
}
