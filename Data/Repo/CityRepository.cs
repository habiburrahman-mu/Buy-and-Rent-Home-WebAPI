using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public CityRepository(BuyRentHomeDbContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _dataContext.Cities.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId)
        {
            return await _dataContext.Cities
                .Where(x => x.CountryId == countryId)
                .ToListAsync();
        }

        public void AddCity(City city)
        {
            _dataContext.Cities.AddAsync(city);
        }
        public async Task<City> FindCity(int id)
        {
            return await _dataContext.Cities.FindAsync(id);
        }
        public void DeleteCity(int cityId)
        {
            var city = _dataContext.Cities.Find(cityId);
            _dataContext.Cities.Remove(city);
        }

        
    }
}
