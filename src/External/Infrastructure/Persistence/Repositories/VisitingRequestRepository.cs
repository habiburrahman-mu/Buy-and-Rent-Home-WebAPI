// Ignore Spelling: Buyand Repo

using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Models;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class VisitingRequestRepository : GenericRepository<VisitingRequest>, IVisitingRequestRepository
    {
        private readonly BuyRentHomeDbContext dbContext;

        public VisitingRequestRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForOwner(int ownerId, string? status = null, int? propertyId = null)
        {
            var query = dbContext.VisitingRequests
                .Include(x => x.Property)
                .Where(x => x.Property.PostedBy == ownerId);

            if (status != null)
            {
                query = query.Where(x => x.Status == status);
            }

            if (propertyId.HasValue)
            {
                query = query.Where(x => x.Property.Id == propertyId.Value);
            }

            var list = await query.Select(x => new VisitingRequestWithPropertyDetailDto
            {
                VisitingRequestId = x.Id,
                PropertyId = x.PropertyId,
                Name = x.Property.Name,
                DateOn = x.DateOn,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                ContactNumber = x.ContactNumber,
                Status = x.Status,
                Notes = x.Notes,
                TakenByName = x.TakenByNavigation.Username
            })
            .OrderBy(x => x.DateOn)
            .ThenBy(x => x.StartTime)
            .AsNoTracking()
            .ToListAsync();
            return list;
        }

        public async Task<bool> IsUserPropertyOwnerOfVisitingRequest(int visitingRequestId, int userId)
        {
            var isUserOwner = await dbContext.VisitingRequests
                .AnyAsync(x => x.Id == visitingRequestId && x.Property.PostedBy == userId);
            //.Include(x => x.Property)
            //.Select(x => x.Property.PostedBy).FirstOrDefaultAsync();
            return isUserOwner;
        }
    }
}
