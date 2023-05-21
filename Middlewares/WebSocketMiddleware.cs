using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Middlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate next;
        private static readonly ConcurrentDictionary<string, WebSocket> _connectedClients = new();

        public WebSocketMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                if (context.Request.Path == "/chat")
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    _connectedClients.TryAdd(webSocket.GetHashCode().ToString(), webSocket);
                    await HandleWebSocketConnection(webSocket);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }
            }
            else
            {
                await next(context);
            }
        }

        private async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    string message = ProcessWebSocketMessage(buffer, result.Count);
                    while (!result.EndOfMessage)
                    {
                        result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                        message += ProcessWebSocketMessage(buffer, result.Count);
                    }
                    await BroadCastMessageToAll(webSocket, message);
                }

            }
            catch
            {

            }
            finally
            {
                _connectedClients.TryRemove(webSocket.GetHashCode().ToString(), out _);
                if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Clpsing", CancellationToken.None);
                }
                webSocket.Dispose();
            }
        }

        private string ProcessWebSocketMessage(byte[] buffer, int count)
        {
            string message = System.Text.Encoding.UTF8.GetString(buffer, 0, count);
            return message;
            //await BroadCastMessageToAll(webSocket, message);
            //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, count), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task BroadCastMessageToAll(WebSocket sender, string message)
        {
            foreach (var client in _connectedClients.Values)
            {
                if (client != sender)
                {
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
                    await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }

    public static class WebSocketMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebSocketMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
