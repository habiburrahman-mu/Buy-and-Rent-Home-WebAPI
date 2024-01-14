using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;

namespace BuyAndRentHomeWebAPI.Data.Repo
{
    public class UserPrivilegeRepository : GenericRepository<UserPrivilege>, IUserPrivilegeRepository
    {
        public UserPrivilegeRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
