using BuyandRentHomeWebAPI.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _dataContext;

        public PropertyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddProperty(Property property)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProperty(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await _dataContext.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Where(p => p.SellRent == sellRent).ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var propertyDetail = await _dataContext.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Where(p => p.Id == id)
                .FirstAsync();
            return propertyDetail;
        }
    }
}
