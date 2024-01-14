using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Repo
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public FurnishingTypeRepository(BuyRentHomeDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await _dataContext.FurnishingTypes.ToListAsync();
        }
    }
}
