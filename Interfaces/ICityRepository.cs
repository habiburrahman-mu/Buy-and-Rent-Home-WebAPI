using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
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
