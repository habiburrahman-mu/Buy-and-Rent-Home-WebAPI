using BuyandRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface ICitiesAreaManagerService
    {
        Task<bool> SaveCitiesAreaManager(CitiesAreaManagerDto citiesAreaManagerDto);
    }
}
