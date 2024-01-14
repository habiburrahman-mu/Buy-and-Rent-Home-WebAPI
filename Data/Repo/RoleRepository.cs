using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;

namespace BuyAndRentHomeWebAPI.Data.Repo
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
