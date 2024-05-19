using Domain.Entities;
using Domain.Interfaces.Data;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public class UserPrivilegeRepository : GenericRepository<UserPrivilege>, IUserPrivilegeRepository
{
    public UserPrivilegeRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
    {
    }
}
