using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface IFurnishingTypeRepository
    {
        Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
    }
}
