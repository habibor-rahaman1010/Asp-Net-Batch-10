using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.ServicesContract
{
    /// <summary>
    /// How a saved notification reaches a browser that is already open. Declared
    /// here and implemented in the web project, so the application layer can push
    /// live without taking a dependency on SignalR.
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Pushes one notification to the people named, along with their own new
        /// unread count. Whoever is not connected simply misses the push and sees it
        /// on their next page load, so this failing must never fail the operation
        /// that raised it.
        /// </summary>
        Task PublishAsync(IEnumerable<Guid> userIds, NotificationDto notification);
    }
}
