using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<Property> GetPropertyDetailAsync(int id);
    }
}
