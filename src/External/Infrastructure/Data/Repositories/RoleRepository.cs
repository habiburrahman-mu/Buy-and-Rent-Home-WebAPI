using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
    {
    }
}
