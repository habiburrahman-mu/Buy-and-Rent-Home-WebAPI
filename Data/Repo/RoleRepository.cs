using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
