using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Somebody's own notifications, and the screen that decides who gets what.
    ///
    /// Everything on the reading side is scoped to the signed-in user, taken off the
    /// principal rather than off anything the browser sent, so there is no request
    /// that can be made to see another person's inbox. The assignment screen is the
    /// one place that touches other people, and it is admin-only.
    /// </summary>
    [Area("Admin"), Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notificationService,
            ApplicationUserManager applicationUserManager,
            ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _applicationUserManager = applicationUserManager;
            _logger = logger;
        }

        /// <summary>The full list of what this person has been told.</summary>
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetNotificationJsonData([FromBody] NotificationListModel model)
        {
            var userId = CurrentUserId();

            if (userId == null)
            {
                return Json(DataTables.EmptyResult);
            }

            var result = await _notificationService.GetPagedAsync(userId.Value,
                model.PageIndex, model.PageSize, model.Search.Value, model.UnreadOnly);

            return Json(new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = result.data.Select(x => new
                {
                    id = x.Id,
                    title = x.Title,
                    message = x.Message,
                    url = x.Url,
                    icon = x.Icon,
                    colour = x.Colour,
                    eventLabel = x.EventLabel,
                    raisedBy = x.RaisedBy,
                    isRead = x.IsRead,
                    created = x.Created.ToString("dd MMM yyyy, HH:mm")
                })
            });
        }

        /// <summary>
        /// What the bell reads on every page load: the unread count and the newest
        /// few. Kept small on purpose, because it runs on every page.
        /// </summary>
        [HttpGet]
        public async Task<JsonResult> Summary()
        {
            var userId = CurrentUserId();

            if (userId == null)
            {
                return Json(new { unreadCount = 0, recent = Array.Empty<object>() });
            }

            try
            {
                var summary = await _notificationService.GetSummaryAsync(userId.Value);

                return Json(new
                {
                    unreadCount = summary.UnreadCount,
                    recent = summary.Recent.Select(x => new
                    {
                        id = x.Id,
                        title = x.Title,
                        message = x.Message,
                        url = x.Url,
                        icon = x.Icon,
                        colour = x.Colour,
                        isRead = x.IsRead,
                        created = x.Created
                    })
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The notification summary could not be read.");

                // The bell is furniture. It goes quiet rather than breaking the page
                // it sits on.
                return Json(new { unreadCount = 0, recent = Array.Empty<object>() });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> MarkAsRead(Guid id)
        {
            var userId = CurrentUserId();

            if (userId == null)
            {
                return Json(new { success = false });
            }

            try
            {
                // False here means the row was not this person's, which is answered
                // the same way as a missing one: nothing happened.
                var marked = await _notificationService.MarkAsReadAsync(id, userId.Value);

                return Json(new { success = marked });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The notification could not be marked as read.");

                return Json(new { success = false });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> MarkAllAsRead()
        {
            var userId = CurrentUserId();

            if (userId == null)
            {
                return Json(new { success = false, marked = 0 });
            }

            try
            {
                var marked = await _notificationService.MarkAllAsReadAsync(userId.Value);

                return Json(new { success = true, marked });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The notifications could not be marked as read.");

                return Json(new { success = false, marked = 0 });
            }
        }

        /// <summary>
        /// Who gets told about what. Admin-only, because this is the one screen that
        /// changes what other people see.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assignments(Guid? userId)
        {
            var model = new NotificationAssignmentViewModel
            {
                Users = await LoadUsersAsync()
            };

            // Opens on whoever was asked for, otherwise on the first person in the
            // list, so the screen is never a set of controls with nothing behind them.
            model.SelectedUserId = userId ?? model.Users.FirstOrDefault()?.Id;

            if (model.SelectedUserId.HasValue)
            {
                model.Assignments = await _notificationService
                    .GetAssignmentsAsync(model.SelectedUserId.Value);
            }

            return View(model);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetAssignments(Guid userId)
        {
            try
            {
                var assignments = await _notificationService.GetAssignmentsAsync(userId);

                return Json(new { success = true, assignments });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The notification assignments could not be read.");

                return Json(new { success = false, message = "The assignments could not be read." });
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<JsonResult> SaveAssignments([FromBody] NotificationAssignmentSaveModel model)
        {
            if (model.UserId == Guid.Empty)
            {
                return Json(new { success = false, message = "Pick somebody to assign first." });
            }

            try
            {
                await _notificationService.SaveAssignmentsAsync(model.UserId, model.Events,
                    User.Identity?.Name ?? string.Empty);

                return Json(new
                {
                    success = true,
                    message = model.Events.Count == 0
                        ? "Taken off every notification."
                        : $"Assigned to {model.Events.Count} notification(s)."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The notification assignments could not be saved.");

                return Json(new { success = false, message = "The assignments could not be saved." });
            }
        }

        /// <summary>
        /// Everybody who can be assigned, with how many events each already has, so
        /// the picker shows at a glance who is covering nothing.
        /// </summary>
        private async Task<IList<NotificationUserModel>> LoadUsersAsync()
        {
            var users = await _applicationUserManager.Users
                .OrderBy(x => x.Email)
                .Select(x => new NotificationUserModel
                {
                    Id = x.Id,
                    Email = x.Email ?? string.Empty,
                    DisplayName = (x.FirstName + " " + x.LastName).Trim()
                })
                .ToListAsync();

            foreach (var user in users)
            {
                // Somebody who never filled in a name still has to be pickable, so
                // the email stands in rather than leaving a blank row.
                if (string.IsNullOrWhiteSpace(user.DisplayName))
                {
                    user.DisplayName = user.Email;
                }

                user.AssignedCount = (await _notificationService.GetAssignmentsAsync(user.Id))
                    .Count(x => x.IsAssigned);
            }

            return users;
        }

        /// <summary>
        /// The signed-in user, read off the principal. Null only when the cookie is
        /// somehow there without an id, which is answered as "nothing to show"
        /// rather than as a fault.
        /// </summary>
        private Guid? CurrentUserId()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(id, out var userId) ? userId : null;
        }
    }
}
