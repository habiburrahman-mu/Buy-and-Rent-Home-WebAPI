using Domain.Entities;

namespace Domain.Interfaces.Data;

public interface IPropertyTypeRepository
{
    Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
}
