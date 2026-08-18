using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DevSkill.Inventory.Web.SignalRHub
{
    /// <summary>
    /// Carries a notification to a browser that is already open. Every connection
    /// joins one group named after its own user id, so a push is addressed to a
    /// person rather than to everybody: nobody can be sent a notification that was
    /// not written for them, whatever the caller passes.
    /// </summary>
    [Authorize]
    public class NotificationHub : Hub
    {
        /// <summary>
        /// The group one user's connections share. Written here rather than in the
        /// publisher so both sides can never name it differently.
        /// </summary>
        public static string GroupFor(Guid userId) => $"user-{userId}";

        public override async Task OnConnectedAsync()
        {
            // Read off the signed-in principal, never off anything the client sent,
            // so a connection cannot subscribe itself to somebody else's group.
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userId, out var id))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, GroupFor(id));
            }

            await base.OnConnectedAsync();
        }

        // Leaving the group on disconnect is not needed: SignalR drops a connection
        // from every group it is in when the connection itself goes.
    }
}
