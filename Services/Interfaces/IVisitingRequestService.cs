using BuyAndRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services.Interfaces
{
    public interface IVisitingRequestService
    {
        Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto);
        Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId);
        Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties(string status = null, int? propertyId = null);
        Task<bool> AcceptVisitingRequest(int visitingRequestId);
    }
}
