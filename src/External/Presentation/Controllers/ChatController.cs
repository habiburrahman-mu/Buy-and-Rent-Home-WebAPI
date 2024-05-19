using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[Authorize]
public class ChatController : BaseController
{
    public ChatController()
    {

    }


}