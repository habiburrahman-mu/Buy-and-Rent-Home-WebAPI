using Domain.Entities;

namespace Domain.Interfaces;

public interface IPropertyTypeRepository
{
    Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
}
