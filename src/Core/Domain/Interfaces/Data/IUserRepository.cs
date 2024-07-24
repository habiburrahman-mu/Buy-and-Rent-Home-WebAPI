using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.Data;

public interface IUserRepository : IGenericRepository<User>
{
    Task<PageResult<User>> GetUserPaginateList(
        int pageNo = 1, int pageSize = 10,
        Expression<Func<User, bool>>? filter = null,
        Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null
        );
    Task<User> GetUserByUserName(string userName);
    void Register(string userName, string email, string password, string mobile);
    Task<bool> UserAlreadyExists(string userName);
}
