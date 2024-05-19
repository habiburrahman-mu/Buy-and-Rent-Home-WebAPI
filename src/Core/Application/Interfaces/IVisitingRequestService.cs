using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IVisitingRequestService
{
    Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto, int currentUserId);
    Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId, int currentUserId);
    Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties(int currentUserId, string? status = null, int? propertyId = null);
    Task<bool> ApproveVisitingRequest(int visitingRequestId, int currentUserId);
    Task<bool> CancelVisitingRequest(CancelVisitingRequestDto cancelVisitingRequestDto, int currentUserId);
}
