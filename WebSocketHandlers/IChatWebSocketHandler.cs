using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.WebSocketHandlers
{
    public interface IChatWebSocketHandler
    {
        Task HandleWebSocketConnection(WebSocket webSocket, HttpContext context);
    }
}
