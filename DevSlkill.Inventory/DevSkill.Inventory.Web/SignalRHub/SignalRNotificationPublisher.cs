using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using Microsoft.AspNetCore.SignalR;

namespace DevSkill.Inventory.Web.SignalRHub
{
    /// <summary>
    /// The SignalR half of <see cref="INotificationPublisher"/>. Lives in the web
    /// project so the application layer never has to know how a push is delivered.
    /// </summary>
    public class SignalRNotificationPublisher : INotificationPublisher
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IInventoryUnitOfWork _notificationUnitOfWork;

        public SignalRNotificationPublisher(IHubContext<NotificationHub> notificationHubContext,
            IInventoryUnitOfWork unitOfWork)
        {
            _notificationHubContext = notificationHubContext;
            _notificationUnitOfWork = unitOfWork;
        }

        public async Task PublishAsync(IEnumerable<Guid> userIds, NotificationDto notification)
        {
            foreach (var userId in userIds.Distinct())
            {
                var group = _notificationHubContext.Clients.Group(NotificationHub.GroupFor(userId));

                // The notification itself is the same for everybody, so it goes out
                // as it stands.
                await group.SendAsync("ReceiveNotification", notification);

                // The badge is not. Somebody with ten unread already must not be told
                // they have one, so each person's own count is read and sent to them.
                var unread = await _notificationUnitOfWork.NotificationRecipientRepository
                    .GetUnreadCountAsync(userId);

                await group.SendAsync("UpdateNotificationCount", unread);
            }
        }
    }
}
