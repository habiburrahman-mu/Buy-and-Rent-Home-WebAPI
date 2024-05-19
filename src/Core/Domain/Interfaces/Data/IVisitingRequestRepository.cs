using Domain.Models;
using Domain.Entities;

namespace Domain.Interfaces.Data
{
    public interface IVisitingRequestRepository : IGenericRepository<VisitingRequest>
    {
        Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForOwner(int ownerId, string? status = null, int? propertyId = null);
        Task<bool> IsUserPropertyOwnerOfVisitingRequest(int visitingRequestId, int userId);
    }
}
