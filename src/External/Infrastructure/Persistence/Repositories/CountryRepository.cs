using Domain.Entities;
using Domain.Interfaces.Data;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public CountryRepository(BuyRentHomeDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var countryList = await _dataContext.Countries.ToListAsync();
            return countryList;
        }
    }
}
