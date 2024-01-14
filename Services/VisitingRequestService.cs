// Ignore Spelling: Buyand

using AutoMapper;
using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using BuyAndRentHomeWebAPI.Specification.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services
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
        public async Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto)
        {
            var visitingRequest = mapper.Map<VisitingRequest>(visitingRequestCreateDto);
            visitingRequest.TakenBy = sharedService.GetUserId();
            visitingRequest.Status = ((char)VisitingRequestStatus.Pending).ToString();
            await unitOfWork.VisitingRequestRepository.Insert(visitingRequest);
            await unitOfWork.SaveAsync();

            var visitingRequestDetailDto = mapper.Map<VisitingRequestDetailDto>(visitingRequest);

            return visitingRequestDetailDto;
        }

        public async Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId)
        {
            var currentUser = sharedService.GetUserId();
            var result = await unitOfWork.VisitingRequestRepository.Get(x => x.TakenBy == currentUser && x.PropertyId == propertyId);
            var visitingRequestDetailDto = mapper.Map<VisitingRequestDetailDto>(result);
            return visitingRequestDetailDto;
        }

        public async Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties()
        {
            var ownerId = sharedService.GetUserId();
            var visitingRequestList = await unitOfWork.VisitingRequestRepository.GetVisitingRequestListForOwner(ownerId);
            return visitingRequestList;
        }
    }
}
