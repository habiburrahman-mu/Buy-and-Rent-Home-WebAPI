using Application.DTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<PageResult<UserDto>> GetUserPaginatedList(PaginationParameter paginationParameter);
    Task<User?> GetUserDetail(LoginRequestDto loginRequest);
    Task<bool> IsUserAlreadyExists(string userName);
    Task<bool> Register(RegisterDto register);
}
