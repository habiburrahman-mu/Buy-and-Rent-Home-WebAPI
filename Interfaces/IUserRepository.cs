using BuyandRentHomeWebAPI.Models;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        void Register(string userName, string password);
        Task<bool> UserAlreadyExists(string userName);
    }
}
