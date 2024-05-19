using Domain.Interfaces.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

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
