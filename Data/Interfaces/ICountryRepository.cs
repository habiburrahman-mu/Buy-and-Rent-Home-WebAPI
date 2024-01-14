using BuyAndRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}
