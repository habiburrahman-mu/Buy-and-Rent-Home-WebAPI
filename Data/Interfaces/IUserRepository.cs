using BuyandRentHomeWebAPI.Data.Entities;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUserName(string userName);
        void Register(string userName, string email, string password, string mobile);
        Task<bool> UserAlreadyExists(string userName);
    }
}
