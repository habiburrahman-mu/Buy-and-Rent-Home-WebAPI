using Domain.ProjectionModels;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVisitingRequestRepository : IGenericRepository<VisitingRequest>
    {
        Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForOwner(int ownerId, string status = null, int? propertyId = null);
        Task<bool> IsUserPropertyOwnerOfVisitingRequest(int visitingRequestId, int userId);
    }
}
