using Application.DTOs;

namespace Application.Interfaces;

public interface IFurnishingTypeService
{
    Task<IEnumerable<KeyValuePairDto>> GetFurnishingTypeList();
}
