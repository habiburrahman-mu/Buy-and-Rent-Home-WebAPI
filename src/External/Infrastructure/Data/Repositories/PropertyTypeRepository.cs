using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

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
