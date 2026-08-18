using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using Microsoft.Extensions.Logging;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Notifications, assigned rather than broadcast. An operation tells this
    /// service what happened; this service looks up who was assigned to that
    /// operation and gives a copy to each of them, and to nobody else.
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IInventoryUnitOfWork _notificationUnitOfWork;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly ILogger<NotificationService> _logger;

        /// <summary>How many the bell shows before sending somebody to the full list.</summary>
        private const int DefaultRecentCount = 6;

        /// <summary>
        /// How long a stock alert stays quiet about the same product. A product below
        /// its line stays below it, so without a window every movement would report
        /// the same product again.
        /// </summary>
        private const int StockAlertQuietHours = 12;

        public NotificationService(IInventoryUnitOfWork unitOfWork,
            INotificationPublisher notificationPublisher,
            ILogger<NotificationService> logger)
        {
            _notificationUnitOfWork = unitOfWork;
            _notificationPublisher = notificationPublisher;
            _logger = logger;
        }

        public async Task RaiseAsync(NotificationEvent notificationEvent, string title, string message,
            string? url = null, Guid? referenceId = null, string? raisedBy = null)
        {
            try
            {
                var subscriberIds = await _notificationUnitOfWork.NotificationSubscriptionRepository
                    .GetSubscriberUserIdsAsync(notificationEvent);

                // Nobody assigned means the operation happens quietly. Writing the
                // event anyway would build a table nobody ever reads.
                if (subscriberIds.Count == 0)
                {
                    return;
                }

                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    Event = notificationEvent,
                    Title = title,
                    Message = message ?? string.Empty,
                    Url = url ?? string.Empty,
                    ReferenceId = referenceId,
                    RaisedBy = raisedBy ?? string.Empty,
                    Created = DateTime.Now
                };

                await _notificationUnitOfWork.NotificationRepository.AddAsync(notification);

                foreach (var userId in subscriberIds)
                {
                    await _notificationUnitOfWork.NotificationRecipientRepository.AddAsync(
                        new NotificationRecipient
                        {
                            Id = Guid.NewGuid(),
                            NotificationId = notification.Id,
                            UserId = userId,
                            IsRead = false
                        });
                }

                await _notificationUnitOfWork.SaveAsync();

                // Pushed only after it is safely saved, so a browser can never show
                // something that is not in the database.
                await PublishQuietlyAsync(subscriberIds, Describe(new NotificationDto
                {
                    Id = notification.Id,
                    Event = notification.Event,
                    Title = notification.Title,
                    Message = notification.Message,
                    Url = notification.Url,
                    RaisedBy = notification.RaisedBy,
                    Created = notification.Created,
                    IsRead = false
                }));
            }
            catch (Exception ex)
            {
                // The operation itself already succeeded. Failing it now because a
                // notification could not be written would be the worse outcome, so
                // this is logged and swallowed.
                _logger.LogError(ex, "The {Event} notification could not be raised.", notificationEvent);
            }
        }

        public async Task RaiseStockAlertsAsync()
        {
            try
            {
                // Read whole rather than filtered, so the two buckets are split on the
                // same snapshot and a product can never land in both.
                var products = await _notificationUnitOfWork.ProductRepository.GetAllAsync();

                var quietSince = DateTime.Now.AddHours(-StockAlertQuietHours);

                foreach (var product in products.Where(x => x.CurrentStock <= x.AlertQuantity))
                {
                    var stockEvent = product.CurrentStock <= 0
                        ? NotificationEvent.OutOfStock
                        : NotificationEvent.LowStockReached;

                    // A product below its line stays below it. Without this the same
                    // product would be reported on every single movement.
                    var alreadyTold = await _notificationUnitOfWork.NotificationRepository
                        .HasRecentAsync(stockEvent, product.Id, quietSince);

                    if (alreadyTold)
                    {
                        continue;
                    }

                    var title = stockEvent == NotificationEvent.OutOfStock
                        ? $"Out of stock: {product.ProductName}"
                        : $"Low stock: {product.ProductName}";

                    var message = stockEvent == NotificationEvent.OutOfStock
                        ? "There is none of this left on the shelf."
                        : $"{product.CurrentStock} left, against an alert quantity of {product.AlertQuantity}.";

                    await RaiseAsync(stockEvent, title, message,
                        $"/Admin/Product/UpdateProduct/{product.Id}", product.Id, "System");
                }
            }
            catch (Exception ex)
            {
                // Same rule as RaiseAsync: the operation that moved the stock has
                // already succeeded and must not be undone by this.
                _logger.LogError(ex, "The stock alerts could not be raised.");
            }
        }

        public async Task<NotificationSummaryDto> GetSummaryAsync(Guid userId, int take = DefaultRecentCount)
        {
            try
            {
                var recent = await _notificationUnitOfWork.NotificationRecipientRepository
                    .GetRecentAsync(userId, take);

                return new NotificationSummaryDto
                {
                    UnreadCount = await _notificationUnitOfWork.NotificationRecipientRepository
                        .GetUnreadCountAsync(userId),
                    Recent = recent.Select(Describe).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<(IList<NotificationDto> data, int total, int totalDisplay)> GetPagedAsync(
            Guid userId, int pageIndex, int pageSize, string? searchValue, bool unreadOnly)
        {
            try
            {
                var result = await _notificationUnitOfWork.NotificationRecipientRepository
                    .GetPagedForUserAsync(userId, pageIndex, pageSize, searchValue, unreadOnly);

                foreach (var row in result.data)
                {
                    Describe(row);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> MarkAsReadAsync(Guid recipientId, Guid userId)
        {
            try
            {
                var marked = await _notificationUnitOfWork.NotificationRecipientRepository
                    .MarkAsReadAsync(recipientId, userId);

                if (marked)
                {
                    await _notificationUnitOfWork.SaveAsync();
                }

                return marked;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<int> MarkAllAsReadAsync(Guid userId)
        {
            try
            {
                var marked = await _notificationUnitOfWork.NotificationRecipientRepository
                    .MarkAllAsReadAsync(userId);

                if (marked > 0)
                {
                    await _notificationUnitOfWork.SaveAsync();
                }

                return marked;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<IList<NotificationAssignmentDto>> GetAssignmentsAsync(Guid userId)
        {
            try
            {
                var assigned = (await _notificationUnitOfWork.NotificationSubscriptionRepository
                    .GetEventsForUserAsync(userId)).ToHashSet();

                var counts = await _notificationUnitOfWork.NotificationSubscriptionRepository
                    .GetSubscriberCountsAsync();

                // Driven off the catalogue rather than off the table, so an event
                // nobody has ever been assigned to still appears on the screen.
                return NotificationEventCatalog.All
                    .Select(info => new NotificationAssignmentDto
                    {
                        Event = info.Event,
                        Group = info.Group,
                        Name = info.Name,
                        Description = info.Description,
                        Icon = info.Icon,
                        Colour = info.Colour,
                        IsAssigned = assigned.Contains(info.Event),
                        SubscriberCount = counts.TryGetValue(info.Event, out var count) ? count : 0
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task SaveAssignmentsAsync(Guid userId, IEnumerable<NotificationEvent> events,
            string assignedBy)
        {
            try
            {
                // Anything the browser sent that is not a real event is dropped here
                // rather than saved as a number nothing will ever raise.
                var known = events.Where(x => Enum.IsDefined(typeof(NotificationEvent), x)).ToList();

                await _notificationUnitOfWork.NotificationSubscriptionRepository
                    .ReplaceForUserAsync(userId, known, assignedBy);

                await _notificationUnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        /// <summary>
        /// Fills in the wording and the icon from the catalogue. Done here rather
        /// than stored on the row, so renaming an event on screen never means
        /// rewriting history.
        /// </summary>
        private static NotificationDto Describe(NotificationDto notification)
        {
            var info = NotificationEventCatalog.Describe(notification.Event);

            notification.Icon = info.Icon;
            notification.Colour = info.Colour;
            notification.EventLabel = info.Name;

            return notification;
        }

        /// <summary>
        /// Pushes to whoever is connected. A push that fails must not undo a
        /// notification that is already saved, so this swallows its own faults.
        /// </summary>
        private async Task PublishQuietlyAsync(IEnumerable<Guid> userIds, NotificationDto notification)
        {
            try
            {
                await _notificationPublisher.PublishAsync(userIds, notification);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "The {Event} notification was saved but could not be pushed.",
                    notification.Event);
            }
        }
    }
}
