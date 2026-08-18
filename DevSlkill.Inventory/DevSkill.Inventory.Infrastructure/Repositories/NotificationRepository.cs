using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification, Guid>, INotificationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public NotificationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<int> DeleteReadOlderThanAsync(DateTime cutoff)
        {
            // Only events whose every copy has been read and is old enough. One
            // unread copy keeps the whole event, because somebody is still owed it.
            var stale = await _inventoryDbContext.Notifications
                .Where(x => x.Created < cutoff
                         && x.Recipients!.All(r => r.IsRead)
                         && x.Recipients!.Any())
                .ToListAsync();

            if (stale.Count == 0)
            {
                return 0;
            }

            _inventoryDbContext.Notifications.RemoveRange(stale);

            return stale.Count;
        }

        public async Task<bool> HasRecentAsync(NotificationEvent notificationEvent, Guid referenceId,
            DateTime since)
        {
            return await _inventoryDbContext.Notifications
                .AnyAsync(x => x.Event == notificationEvent
                            && x.ReferenceId == referenceId
                            && x.Created >= since);
        }
    }
}
