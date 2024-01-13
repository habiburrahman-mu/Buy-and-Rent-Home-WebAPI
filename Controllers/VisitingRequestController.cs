using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuyandRentHomeWebAPI.Controllers
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
    }
}
