using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models.Notification;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Bike360.UI.Layout
{
    public partial class LayoutHeader
    {
        [Inject]
        ICustomNotificationService NotificationService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        IMessageService _message { get; set; }

        bool showNotifications = false;
        public List<NotificationVM> Notifications { get; set; } = [];
        protected override async Task OnInitializedAsync()
        {
            var userId = await GetLoggedInUserId();

            Notifications = await NotificationService.GetUserNotifications(userId);

            foreach (var notification in Notifications)
            {
                notification.ViewMessage = await GetName(notification);
            }
        }

        protected async Task OpenNotificationsDrawer()
        {
            showNotifications = true;
        }

        public async Task<string> GetLoggedInUserId()
        {
            if (AuthStateProvider != null)
            {
                var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                return user.FindFirst("uid").Value;
            }

            return null;
        }

        private async Task<string> GetName(NotificationVM notification)
        {
            return await NotificationService.MapNotificationName(notification);
        }

        private async Task DeleteNotification(int notificationId)
        {
            var response = await NotificationService.Delete(notificationId);
            if (response.IsSuccess)
            {
                Notifications.RemoveAll(notification => notification.Id == notificationId);
            }
            else
            {
                _message.Error("Nie mo¿na usun¹æ powiadomienia");
            }
        }

        private async void SetNotificationCustomersList(NotificationVM notification)
        {
            if (notification.Customers.Count == 0)
            {
                notification.Customers = await NotificationService.NotificationsCustomers(notification);
                StateHasChanged();
            }
        }
    }
}