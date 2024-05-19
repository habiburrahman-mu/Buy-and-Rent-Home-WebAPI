using Domain.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

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
