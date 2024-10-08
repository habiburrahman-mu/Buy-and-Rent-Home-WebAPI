using Domain.Interfaces.Services;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationsHub, INotificationClient> hubContext;

        public NotificationService(IHubContext<NotificationsHub, INotificationClient> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendNotificationToAll(string message)
        {
            await hubContext.Clients.All.ReceiveNotification(message);
        }

        public async Task SendNotification(int userId, string message)
        {
            await hubContext.Clients.User(userId.ToString()).ReceiveNotification(message);
        }
    }
}
