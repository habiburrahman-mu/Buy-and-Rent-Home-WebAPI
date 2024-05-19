using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public BaseController()
    {
        
    }
}
