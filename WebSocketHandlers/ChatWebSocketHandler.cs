using BuyAndRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.WebSocketHandlers
{
    public class ChatWebSocketHandler : IChatWebSocketHandler
    {
        private static readonly ConcurrentDictionary<int, List<WebSocket>> _connectedClients = new();
        private readonly IServiceProvider serviceProvider;

        public ChatWebSocketHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task HandleWebSocketConnection(WebSocket webSocket, HttpContext context)
        {

            //var currentUserIdString = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //Int32.TryParse(currentUserIdString, out int currentUserId);

            int currentUserId = 0;

            using (var scope = serviceProvider.CreateScope())
            {
                var sharedService = scope.ServiceProvider.GetService<ISharedService>();
                currentUserId = sharedService.GetUserId();
            }

            var socketListForCurrentUser = _connectedClients.GetOrAdd(currentUserId, new List<WebSocket>());
            socketListForCurrentUser.Add(webSocket);
            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    await HandleMessageAsync(webSocket, currentUserId);
                }
            }
            catch (WebSocketException)
            {
                socketListForCurrentUser.Remove(webSocket);
            }
            finally
            {
                if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived)
                {
                    socketListForCurrentUser.Remove(webSocket);
                    if (socketListForCurrentUser.Count == 0)
                    {
                        _connectedClients.TryRemove(currentUserId, out _);
                    }
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
                webSocket.Dispose();
            }
        }

        private async Task HandleMessageAsync(WebSocket webSocket, int sender)
        {
            byte[] buffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            string message = ProcessWebSocketMessage(buffer, result.Count);
            while (!result.EndOfMessage)
            {
                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                message += ProcessWebSocketMessage(buffer, result.Count);
            }
            await BroadCastMessageToAll(sender, message);

        }

        private string ProcessWebSocketMessage(byte[] buffer, int count)
        {
            string message = System.Text.Encoding.UTF8.GetString(buffer, 0, count);
            return message;
            //await BroadCastMessageToAll(webSocket, message);
            //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, count), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task BroadCastMessageToAll(int sender, string message)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Key != sender)
                {
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);

                    var socketList = client.Value;
                    var tasks = socketList.Select(socket =>
                    {
                        return socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    });
                    await Task.WhenAll(tasks);
                }
            }
        }
    }
}
