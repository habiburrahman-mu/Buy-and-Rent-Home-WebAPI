using BuyAndRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<Property> GetPropertyDetailAsync(int id);
    }
}
