using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;

namespace BuyandRentHomeWebAPI.Services
{
    public class SharedService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SharedService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            string userId = _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!String.IsNullOrEmpty(userId))
            {
                return Convert.ToInt32(userId);
            }
            return 0;
        }
    }
}
