using Application.DTOs;

namespace Application.Interfaces;

public interface IPropertyTypeService
{
    Task<IEnumerable<KeyValuePairDto>> GetPropertyTypeList();
}
