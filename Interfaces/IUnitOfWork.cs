using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        Task<bool> SaveAsync();
    }
}
