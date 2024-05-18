using Domain.Entities;

namespace Domain.Interfaces;

public interface IFurnishingTypeRepository
{
    Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
}
