using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
    }
}
