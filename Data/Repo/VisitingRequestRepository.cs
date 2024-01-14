using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class VisitingRequestRepository : GenericRepository<VisitingRequest>, IVisitingRequestRepository
    {
        private readonly BuyRentHomeDbContext dbContext;

        public VisitingRequestRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForOwner(int postedBy)
        {
            var list = await dbContext.VisitingRequests
                .Include(x => x.Property)
                .Where(x => x.Property.PostedBy == postedBy)
                .Select(x => new VisitingRequestWithPropertyDetailDto
                {
                    VisitingRequestId = x.Id,
                    PropertyId = x.PropertyId,
                    Name = x.Property.Name,
                    DateOn = x.DateOn,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    ContactNumber = x.ContactNumber,
                    Status = x.Status,
                    Notes = x.Notes
                })
                .OrderBy(x => x.StartTime)
                .AsNoTracking()
                .ToListAsync();
            return list;
        }
    }
}
