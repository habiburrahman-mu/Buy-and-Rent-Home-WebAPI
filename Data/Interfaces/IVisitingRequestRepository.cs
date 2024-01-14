using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IVisitingRequestRepository : IGenericRepository<VisitingRequest>
    {
        Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForOwner(int postedBy);
    }
}
