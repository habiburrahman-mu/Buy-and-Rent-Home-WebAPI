using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class VisitingRequestRepository : GenericRepository<VisitingRequest>, IVisitingRequestRepository
    {
        public VisitingRequestRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
