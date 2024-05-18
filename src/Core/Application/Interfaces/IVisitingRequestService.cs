using Application.DTOs;
using Domain.ProjectionModels;

namespace Application.Interfaces;

public interface IVisitingRequestService
{
    Task<VisitingRequestDetailDto> CreateVisitingRequest(VisitingRequestCreateDto visitingRequestCreateDto);
    Task<VisitingRequestDetailDto> GetVisitingRequestDetailForCurrentUserByPropertyId(int propertyId);
    Task<List<VisitingRequestWithPropertyDetailDto>> GetVisitingRequestListForMyProperties(string? status = null, int? propertyId = null);
    Task<bool> ApproveVisitingRequest(int visitingRequestId);
    Task<bool> CancelVisitingRequest(CancelVisitingRequestDto cancelVisitingRequestDto);
}
