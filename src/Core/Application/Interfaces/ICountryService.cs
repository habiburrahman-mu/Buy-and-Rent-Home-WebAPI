using Application.DTOs;

namespace Application.Interfaces;

public interface ICountryService
{
    Task<List<CountryDto>> GetCountryList();
}
