using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuyAndRentHomeWebAPI.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        public ChatController()
        {
                
        }


    }
}
