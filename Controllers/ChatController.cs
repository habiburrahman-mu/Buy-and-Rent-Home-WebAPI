using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuyandRentHomeWebAPI.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        public ChatController()
        {
                
        }


    }
}
