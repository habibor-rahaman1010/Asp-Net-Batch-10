using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class NotificationRecipientRepository
        : Repository<NotificationRecipient, Guid>, INotificationRecipientRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public NotificationRecipientRepository(InventoryDbContext inventoryDbContext)
            : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _inventoryDbContext.NotificationRecipients
                .CountAsync(x => x.UserId == userId && !x.IsRead);
        }

        public async Task<IList<NotificationDto>> GetRecentAsync(Guid userId, int take)
        {
            return await OwnedBy(userId)
                .OrderByDescending(x => x.Notification!.Created)
                .Take(take)
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<(IList<NotificationDto> data, int total, int totalDisplay)> GetPagedForUserAsync(
            Guid userId, int pageIndex, int pageSize, string? searchValue, bool unreadOnly)
        {
            var mine = OwnedBy(userId);

            // Counted before the search so the table can say "showing x of y" against
            // everything this person has, not against what the search left.
            var total = await mine.CountAsync();

            if (unreadOnly)
            {
                mine = mine.Where(x => !x.IsRead);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                mine = mine.Where(x => x.Notification!.Title.Contains(searchValue)
                                    || x.Notification!.Message.Contains(searchValue)
                                    || x.Notification!.RaisedBy.Contains(searchValue));
            }

            var totalDisplay = await mine.CountAsync();

            var data = await mine
                .OrderByDescending(x => x.Notification!.Created)
                // Page index is 1-based here, the same as every other paged read.
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(ToDto)
                .ToListAsync();

            return (data, total, totalDisplay);
        }

        public async Task<bool> MarkAsReadAsync(Guid recipientId, Guid userId)
        {
            // The user id is part of the lookup rather than checked afterwards, so a
            // guessed id simply finds nothing.
            var recipient = await _inventoryDbContext.NotificationRecipients
                .FirstOrDefaultAsync(x => x.Id == recipientId && x.UserId == userId);

            if (recipient == null)
            {
                return false;
            }

            // Already read is left alone, so the time it was first read is not moved
            // by opening the list again.
            if (!recipient.IsRead)
            {
                recipient.IsRead = true;
                recipient.ReadAt = DateTime.Now;
            }

            return true;
        }

        public async Task<int> MarkAllAsReadAsync(Guid userId)
        {
            var unread = await _inventoryDbContext.NotificationRecipients
                .Where(x => x.UserId == userId && !x.IsRead)
                .ToListAsync();

            var now = DateTime.Now;

            foreach (var recipient in unread)
            {
                recipient.IsRead = true;
                recipient.ReadAt = now;
            }

            return unread.Count;
        }

        /// <summary>
        /// The one place a user id turns into rows. Every read goes through here, so
        /// no query can accidentally be written without the ownership filter.
        /// </summary>
        private IQueryable<NotificationRecipient> OwnedBy(Guid userId)
        {
            return _inventoryDbContext.NotificationRecipients
                .Include(x => x.Notification)
                .Where(x => x.UserId == userId);
        }

        /// <summary>
        /// Shared projection so the bell and the full list cannot drift apart. The
        /// icon and the label are left empty here and filled in from the catalogue
        /// by the service, because the database has no business knowing them.
        /// </summary>
        private static readonly System.Linq.Expressions.Expression<Func<NotificationRecipient, NotificationDto>>
            ToDto = x => new NotificationDto
            {
                Id = x.Id,
                Event = x.Notification!.Event,
                Title = x.Notification!.Title,
                Message = x.Notification!.Message,
                Url = x.Notification!.Url,
                RaisedBy = x.Notification!.RaisedBy,
                Created = x.Notification!.Created,
                IsRead = x.IsRead
            };
    }
}
