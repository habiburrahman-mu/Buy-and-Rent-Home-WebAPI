using Domain.Entities;

namespace Domain.Interfaces.Data;

public interface IFurnishingTypeRepository
{
    Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
}
