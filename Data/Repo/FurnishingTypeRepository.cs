using BuyandRentHomeWebAPI.Interfaces;
using BuyandRentHomeWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DataContext _dataContext;

        public FurnishingTypeRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await _dataContext.FurnishingTypes.ToListAsync();
        }
    }
}
