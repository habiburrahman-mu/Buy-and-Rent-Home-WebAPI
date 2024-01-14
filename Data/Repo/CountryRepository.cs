﻿using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Repo
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
