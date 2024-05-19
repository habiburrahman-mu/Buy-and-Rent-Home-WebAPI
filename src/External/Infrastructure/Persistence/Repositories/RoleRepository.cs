using Domain.Entities;
using Domain.Interfaces.Data;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
    {
    }
}
