using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class UserPrivilegeRepository : GenericRepository<UserPrivilege>, IUserPrivilegeRepository
{
    public UserPrivilegeRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
    {
    }
}
