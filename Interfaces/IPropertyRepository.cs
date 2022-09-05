using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent);
        Task<Property> GetPropertyDetailAsync(int id);
        void AddProperty(Property property);
        void DeleteProperty(int id);
    }
}
