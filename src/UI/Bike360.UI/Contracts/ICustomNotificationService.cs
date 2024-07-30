using Bike360.UI.Models.Notification;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts;

public interface ICustomNotificationService
{
    Task<Response<Guid>> Delete(int id);
    Task<List<NotificationVM>> GetUserNotifications(string userId);
    void ClearNotifications();
    Task<string> MapNotificationName(NotificationVM notification);
    Task<List<NotificationCustomerVM>> NotificationsCustomers(NotificationVM notification);
}
