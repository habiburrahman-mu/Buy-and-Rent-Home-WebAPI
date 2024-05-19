using Application.Interfaces;
using Domain.Models;
using Domain.Entities;
using Domain.Interfaces.Data;
using Application.DTOs;
using AutoMapper;
using Common.Constants;
using Domain.Exceptions;

namespace Application.Services;

public class VisitingRequestService : IVisitingRequestService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public VisitingRequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto, int currentUserId)
    {
        var visitingRequest = mapper.Map<VisitingRequest>(visitingRequestCreateDto);
        visitingRequest.TakenBy = currentUserId;
        visitingRequest.Status = ((char)VisitingRequestStatus.Pending).ToString();
        await unitOfWork.VisitingRequestRepository.Insert(visitingRequest);
        await unitOfWork.SaveAsync();

        var visitingRequestDetailDto = mapper.Map<VisitingRequestDetailDto>(visitingRequest);
        visitingRequestDetailDto.StartTime = new DateTime(visitingRequestDetailDto.StartTime.Ticks);
        visitingRequestDetailDto.EndTime = new DateTime(visitingRequestDetailDto.EndTime.Ticks);
        //visitingRequestDetailDto.DateOn = visitingRequestDetailDto.DateOn;

        return visitingRequestDetailDto;
    }

    public async Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId, int currentUserId)
    {
        var currentUser = currentUserId;
        var result = await unitOfWork.VisitingRequestRepository.Get(x => x.TakenBy == currentUser && x.PropertyId == propertyId);
        var visitingRequestDetailDto = mapper.Map<VisitingRequestDetailDto>(result);
        return visitingRequestDetailDto;
    }

    public async Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties(int currentUserId, string? status = null, int? propertyId = null)
    {
        var ownerId = currentUserId;
        var visitingRequestList = await unitOfWork.VisitingRequestRepository.GetVisitingRequestListForOwner(ownerId, status, propertyId);
        return visitingRequestList;
    }

    public async Task<bool> ApproveVisitingRequest(int visitingRequestId, int currentUserId)
    {
        var ownerId = currentUserId;
        var visitingRequest = await unitOfWork.VisitingRequestRepository.Get(x => x.Id == visitingRequestId);
        await ValidateVisitingRequest(visitingRequestId, ownerId, visitingRequest);

        visitingRequest.Status = ((char)VisitingRequestStatus.Approved).ToString();

        unitOfWork.VisitingRequestRepository.Update(visitingRequest);

        return await unitOfWork.SaveAsync();
    }

    public async Task<bool> CancelVisitingRequest(CancelVisitingRequestDto cancelVisitingRequestDto, int currentUserId)
    {
        var ownerId = currentUserId;
        var visitingRequest = await unitOfWork.VisitingRequestRepository.Get(x => x.Id == cancelVisitingRequestDto.VisitingRequestId);
        await ValidateVisitingRequest(cancelVisitingRequestDto.VisitingRequestId, ownerId, visitingRequest);

        visitingRequest.Status = ((char)VisitingRequestStatus.NotApproved).ToString();
        visitingRequest.Notes = cancelVisitingRequestDto.CancelReason;

        unitOfWork.VisitingRequestRepository.Update(visitingRequest);

        return await unitOfWork.SaveAsync();
    }

    private async Task ValidateVisitingRequest(int visitingRequestId, int ownerId, VisitingRequest? visitingRequest)
    {
        if (visitingRequest == null)
            throw new InvalidDomainRequestException("Visiting request not found.");

        if (!await unitOfWork.VisitingRequestRepository.IsUserPropertyOwnerOfVisitingRequest(visitingRequestId, ownerId))
        {
            throw new UnauthorizedAccessException("Not authorized.");
        }

        if (visitingRequest.Status == ((char)VisitingRequestStatus.Approved).ToString() || visitingRequest.Status == ((char)VisitingRequestStatus.NotApproved).ToString())
            throw new UnauthorizedAccessException("Not allowed to change visiting request.");
    }
}
