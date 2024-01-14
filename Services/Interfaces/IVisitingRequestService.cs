using BuyandRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IVisitingRequestService
    {
        Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto);
        Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId);
        Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties();
    }
}
