using Domain.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

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

    public async Task<bool> ChangePropertyStatus(int id, string status)
    {
        using var dbTransaction = await _dataContext.Database.BeginTransactionAsync();
        try
        {
            // if new status for property is complete
            // mark all pending visiting request to not approved
            if (status == "C")
            {
                await _dataContext.VisitingRequests
                .Where(x => x.PropertyId == id && x.Status == "P")
                .ExecuteUpdateAsync(p =>
                    p.SetProperty(cols => cols.Status, "N")
                    .SetProperty(cols => cols.Notes, "Property sold/rented"));
            }


            await _dataContext.Properties
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(p => p.SetProperty(cols => cols.Status, status));

            await dbTransaction.CommitAsync();
            return true;
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            return false;
        }

    }
}
