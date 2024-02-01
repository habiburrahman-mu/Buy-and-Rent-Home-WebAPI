// Ignore Spelling: Buyand

using AutoMapper;
using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using BuyAndRentHomeWebAPI.Specification.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        public async Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties(string status = null, int? propertyId = null)
        {
            var ownerId = sharedService.GetUserId();
            var visitingRequestList = await unitOfWork.VisitingRequestRepository.GetVisitingRequestListForOwner(ownerId, status, propertyId);
            return visitingRequestList;
        }

        public async Task<bool> UpdateVisitingRequestStatus(int visitingRequestId, char status)
        {
            var ownerId = sharedService.GetUserId();
            var visitingRequest = await unitOfWork.VisitingRequestRepository.Get(x => x.Id == visitingRequestId);
            await ValidateVisitingRequest(visitingRequestId, ownerId, visitingRequest);

            visitingRequest.Status = status switch
            {
                ((char)VisitingRequestStatus.Approved) => ((char)VisitingRequestStatus.Approved).ToString(),
                ((char)VisitingRequestStatus.NotApproved) => ((char)VisitingRequestStatus.NotApproved).ToString(),
                _ => throw new BadHttpRequestException("Visiting request not valid.")
            };

            //visitingRequest.Status = ((char)VisitingRequestStatus.Approved).ToString();

            unitOfWork.VisitingRequestRepository.Update(visitingRequest);

            return await unitOfWork.SaveAsync();
        }

        private async Task ValidateVisitingRequest(int visitingRequestId, int ownerId, VisitingRequest visitingRequest)
        {
            if (visitingRequest == null)
                throw new BadHttpRequestException("Visiting request not found.");

            if (!await unitOfWork.VisitingRequestRepository.IsUserPropertyOwnerOfVisitingRequest(visitingRequestId, ownerId))
            {
                throw new BadHttpRequestException("Not authorized.");
            }

            if (visitingRequest.Status == ((char)VisitingRequestStatus.Approved).ToString() || visitingRequest.Status == ((char)VisitingRequestStatus.NotApproved).ToString())
                throw new BadHttpRequestException("Not allowed to change visiting request.");
        }
    }
}
