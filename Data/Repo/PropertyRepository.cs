using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public PropertyRepository(BuyRentHomeDbContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
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
