// Ignore Spelling: Buyand

using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Interfaces
{
    public interface IVisitingRequestRepository : IGenericRepository<VisitingRequest>
    {
        Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForOwner(int ownerId, string status = null, int? propertyId = null);
        Task<bool> IsUserPropertyOwnerOfVisitingRequest(int visitingRequestId, int userId);
    }
}
