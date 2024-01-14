using BuyAndRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId);
        void AddCity(City city);
        Task<City> FindCity(int id);
        void DeleteCity(int cityId);
    }
}
