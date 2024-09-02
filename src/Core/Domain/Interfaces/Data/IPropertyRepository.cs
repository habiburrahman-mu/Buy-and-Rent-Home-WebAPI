using Domain.Entities;

namespace Domain.Interfaces.Data;

public interface IPropertyRepository : IGenericRepository<Property>
{
    Task<Property> GetPropertyDetailAsync(int id);
    Task<bool> ChangePropertyStatus(int id, string status);
}
