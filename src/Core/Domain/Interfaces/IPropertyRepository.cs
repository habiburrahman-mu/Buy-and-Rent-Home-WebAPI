using Domain.Entities;

namespace Domain.Interfaces;

public interface IPropertyRepository : IGenericRepository<Property>
{
    Task<Property> GetPropertyDetailAsync(int id);
}
