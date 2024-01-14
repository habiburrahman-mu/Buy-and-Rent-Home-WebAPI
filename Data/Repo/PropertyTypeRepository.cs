using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Repo
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public PropertyTypeRepository(BuyRentHomeDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await _dataContext.PropertyTypes.ToListAsync();
        }
    }
}
