using AutoMapper;
using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using BuyandRentHomeWebAPI.Specification.Constants;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services
{
    public class VisitingRequestService : IVisitingRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISharedService sharedService;

        public VisitingRequestService(IUnitOfWork unitOfWork, IMapper mapper, ISharedService sharedService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.sharedService = sharedService;
        }
        public async Task<int> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto)
        {
            var visitingRequest = mapper.Map<VisitingRequest>(visitingRequestCreateDto);
            visitingRequest.TakenBy = sharedService.GetUserId();
            visitingRequest.Status = ((char)VisitingRequestStatus.Pending).ToString();
            await unitOfWork.VisitingRequestRepository.Insert(visitingRequest);
            await unitOfWork.SaveAsync();
            return visitingRequest.Id;
        }
    }
}
