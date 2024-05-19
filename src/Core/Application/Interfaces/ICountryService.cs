using Application.DTOs;

namespace Application.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetCountryList();
}
