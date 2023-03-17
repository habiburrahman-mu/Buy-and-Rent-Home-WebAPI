using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<PageResult<UserDto>> GetUserPaginatedList(PaginationParameter paginationParameter);
        Task<User> Authenticate(LoginRequestDto loginRequest);
        LoginResponseDto CreateLoginCredintials(User user);
        Task<bool> UserAlreadyExists(string userName);
        Task<bool> Register(RegisterDto register);
    }
}
