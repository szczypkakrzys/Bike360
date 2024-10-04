using Bike360.UI.Services.Base;

namespace Bike360.UI.Models.Notification;

public class NotificationVM
{
    public int Id { get; set; }
    public string Message { get; set; }
    public int? ActivityId { get; set; }
    public IEnumerable<int>? CustomersIdsList { get; set; }
    public NotificationType Type { get; set; }
    public DateTime Date { get; set; }
    public string ViewMessage { get; set; }
    public List<NotificationCustomerVM> Customers { get; set; } = [];
}
