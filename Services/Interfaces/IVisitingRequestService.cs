using BuyandRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IVisitingRequestService
    {
        Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto);
        Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId);
    }
}
