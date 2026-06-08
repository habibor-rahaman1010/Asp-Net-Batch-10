using DevSkill.Inventory.Application.ServicesContract;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DevSkill.Inventory.Web.SignalRHub
{
    public class PresenceUserHub : Hub
    {
        private readonly IOnlineUserTrackerService _onlineUserTrackerService;

        public PresenceUserHub(IOnlineUserTrackerService onlineUserTrackerService)
        {
            _onlineUserTrackerService = onlineUserTrackerService;
        }


        public override async Task OnConnectedAsync()
        {
            var userId = Context?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userId, out Guid id))
            {
                _onlineUserTrackerService.UserConnected(id, Context?.ConnectionId!);

                await Clients.All.SendAsync("UpdateOnlineUsers", _onlineUserTrackerService.GetOnlineUserCount());
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _onlineUserTrackerService.UserDisconnected(Context.ConnectionId);

            await Clients.All.SendAsync("UpdateOnlineUsers", _onlineUserTrackerService.GetOnlineUserCount());

            await base.OnDisconnectedAsync(exception);
        }
    }
}