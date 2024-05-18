using Domain.Entities;

namespace Domain.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetCountriesAsync();
}
