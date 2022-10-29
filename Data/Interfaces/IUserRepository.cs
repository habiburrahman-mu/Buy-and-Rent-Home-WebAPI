using BuyandRentHomeWebAPI.Models;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        void Register(string userName, string email, string password, string mobile);
        Task<bool> UserAlreadyExists(string userName);
    }
}
