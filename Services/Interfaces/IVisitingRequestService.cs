using BuyandRentHomeWebAPI.Dtos;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IVisitingRequestService
    {
        Task<int> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto);
    }
}
