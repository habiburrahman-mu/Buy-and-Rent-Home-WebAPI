
namespace Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotification(int userId, string message);
        Task SendNotificationToAll(string message);
    }
}
