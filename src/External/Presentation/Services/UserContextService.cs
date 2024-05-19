using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services;

public interface IUserContextService
{
    int GetUserId();
}

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        Claim? identifierClaim = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier);
        int userId = 0;
        int.TryParse(identifierClaim?.Value, out userId);
        return userId;
    }
}
