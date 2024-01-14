using BuyAndRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services.Interfaces
{
    public interface ICitiesAreaManagerService
    {
        Task<bool> SaveCitiesAreaManager(CitiesAreaManagerDto citiesAreaManagerDto);
    }
}
