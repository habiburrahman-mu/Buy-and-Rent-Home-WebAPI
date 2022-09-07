using BuyandRentHomeWebAPI.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DataContext _dataContext;

        public PropertyTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await _dataContext.PropertyTypes.ToListAsync();
        }
    }
}
