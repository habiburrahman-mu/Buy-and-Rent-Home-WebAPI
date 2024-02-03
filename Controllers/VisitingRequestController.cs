using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyAndRentHomeWebAPI.Controllers
{
    public class VisitingRequestController : BaseController
    {
        private readonly IVisitingRequestService visitingRequestService;

        public VisitingRequestController(IVisitingRequestService visitingRequestService)
        {
            this.visitingRequestService = visitingRequestService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetVisitingRequestDetailForCurrentUser/{propertyId}")]
        public async Task<IActionResult> GetVisitingRequestDetailForCurrentUser(int propertyId)
        {
            var result = await visitingRequestService.GetVisitingRequestDetailForCurrentUserByPropertyId(propertyId);
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(VisitingRequestCreateDto visitingRequestCreateDto)
        {
            var visitingRequest = await visitingRequestService.CreateVisitingRequest(visitingRequestCreateDto);
            return Ok(visitingRequest);
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetVisitingRequestListForMyProperties")]
        public async Task<IActionResult> GetVisitingRequestListForMyProperties([FromQuery] string status = null, [FromQuery] int? propertyId = null)
        {
            var list = await visitingRequestService.GetVisitingRequestListForMyProperties(status, propertyId);
            return Ok(list);
        }

        [Authorize(Roles = "User")]
        [HttpPut("ApproveVisitingRequest")]
        public async Task<IActionResult> ApproveVisitingRequest([FromBody] int visitingRequestId)
        {
            var response = await visitingRequestService.ApproveVisitingRequest(visitingRequestId);
            return Ok(response);
        }

        [Authorize(Roles = "User")]
        [HttpPut("CancelVisitingRequest")]
        public async Task<IActionResult> CancelVisitingRequest([FromBody] CancelVisitingRequestDto cancelVisitingRequestDto)
        {
            var response = await visitingRequestService.CancelVisitingRequest(cancelVisitingRequestDto);
            return Ok(response);
        }
    }
}
