using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub<INotificationClient>
    {

    }

    public interface INotificationClient
    {
        Task ReceiveNotification(string notification);
    }
}
