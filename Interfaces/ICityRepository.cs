using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        Task<City> FindCity(int id);
        void DeleteCity(int cityId);
    }
}
