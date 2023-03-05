using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class UserPrivilegeRepository : GenericRepository<UserPrivilege>, IUserPrivilegeRepository
    {
        public UserPrivilegeRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
