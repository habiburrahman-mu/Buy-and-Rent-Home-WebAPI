using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services;

namespace Presentation.Controllers;

public class VisitingRequestController : BaseController
{
    private readonly IVisitingRequestService visitingRequestService;
    private readonly IUserContextService userContextService;

    public VisitingRequestController(IVisitingRequestService visitingRequestService, IUserContextService userContextService)
    {
        this.visitingRequestService = visitingRequestService;
        this.userContextService = userContextService;
    }

    [Authorize(Roles = "User")]
    [HttpGet("GetVisitingRequestDetailForCurrentUser/{propertyId}")]
    public async Task<IActionResult> GetVisitingRequestDetailForCurrentUser(int propertyId)
    {
        var result = await visitingRequestService.GetVisitingRequestDetailForCurrentUserByPropertyId(propertyId, userContextService.GetUserId());
        return Ok(result);
    }

    [Authorize(Roles = "User")]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(VisitingRequestCreateDto visitingRequestCreateDto)
    {
        var visitingRequest = await visitingRequestService.CreateVisitingRequest(visitingRequestCreateDto, userContextService.GetUserId());
        return Ok(visitingRequest);
    }

    [Authorize(Roles = "User")]
    [HttpGet("GetVisitingRequestListForMyProperties")]
    public async Task<IActionResult> GetVisitingRequestListForMyProperties([FromQuery] string? status = null, [FromQuery] int? propertyId = null)
    {
        var list = await visitingRequestService.GetVisitingRequestListForMyProperties(userContextService.GetUserId(), status, propertyId);
        return Ok(list);
    }

    [Authorize(Roles = "User")]
    [HttpPut("ApproveVisitingRequest")]
    public async Task<IActionResult> ApproveVisitingRequest([FromBody] int visitingRequestId)
    {
        var response = await visitingRequestService.ApproveVisitingRequest(visitingRequestId, userContextService.GetUserId());
        return Ok(response);
    }

    [Authorize(Roles = "User")]
    [HttpPut("CancelVisitingRequest")]
    public async Task<IActionResult> CancelVisitingRequest([FromBody] CancelVisitingRequestDto cancelVisitingRequestDto)
    {
        var response = await visitingRequestService.CancelVisitingRequest(cancelVisitingRequestDto, userContextService.GetUserId());
        return Ok(response);
    }
}
