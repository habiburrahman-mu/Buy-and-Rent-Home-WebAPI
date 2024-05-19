using Domain.Entities;

namespace Domain.Interfaces.Data;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetCountriesAsync();
}
