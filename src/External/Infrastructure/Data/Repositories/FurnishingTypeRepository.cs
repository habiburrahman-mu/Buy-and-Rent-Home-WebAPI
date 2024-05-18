using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

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
