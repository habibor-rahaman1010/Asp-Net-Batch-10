using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class NotificationSubscriptionRepository
        : Repository<NotificationSubscription, Guid>, INotificationSubscriptionRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public NotificationSubscriptionRepository(InventoryDbContext inventoryDbContext)
            : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<Guid>> GetSubscriberUserIdsAsync(NotificationEvent notificationEvent)
        {
            return await _inventoryDbContext.NotificationSubscriptions
                .Where(x => x.Event == notificationEvent)
                .Select(x => x.UserId)
                // The same person assigned twice would otherwise be told twice.
                .Distinct()
                .ToListAsync();
        }

        public async Task<IList<NotificationEvent>> GetEventsForUserAsync(Guid userId)
        {
            return await _inventoryDbContext.NotificationSubscriptions
                .Where(x => x.UserId == userId)
                .Select(x => x.Event)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IDictionary<NotificationEvent, int>> GetSubscriberCountsAsync()
        {
            var counts = await _inventoryDbContext.NotificationSubscriptions
                .GroupBy(x => x.Event)
                .Select(g => new { g.Key, Count = g.Select(x => x.UserId).Distinct().Count() })
                .ToListAsync();

            return counts.ToDictionary(x => x.Key, x => x.Count);
        }

        public async Task ReplaceForUserAsync(Guid userId, IEnumerable<NotificationEvent> events,
            string assignedBy)
        {
            var wanted = events.Distinct().ToList();

            var existing = await _inventoryDbContext.NotificationSubscriptions
                .Where(x => x.UserId == userId)
                .ToListAsync();

            // Rows that are staying are left alone rather than deleted and rewritten,
            // so the date an assignment was first made survives an unrelated edit.
            var toRemove = existing.Where(x => !wanted.Contains(x.Event)).ToList();
            var alreadyThere = existing.Select(x => x.Event).ToHashSet();

            if (toRemove.Count > 0)
            {
                _inventoryDbContext.NotificationSubscriptions.RemoveRange(toRemove);
            }

            var now = DateTime.Now;

            foreach (var notificationEvent in wanted.Where(x => !alreadyThere.Contains(x)))
            {
                await _inventoryDbContext.NotificationSubscriptions.AddAsync(new NotificationSubscription
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Event = notificationEvent,
                    Created = now,
                    AssignedBy = assignedBy
                });
            }
        }
    }
}
