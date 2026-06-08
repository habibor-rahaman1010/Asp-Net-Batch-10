using DevSkill.Inventory.Application.ServicesContract;
using System.Collections.Concurrent;

namespace DevSkill.Inventory.Application.Services
{
    public class OnlineUserTrackerService : IOnlineUserTrackerService
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _users = new ConcurrentDictionary<string, HashSet<string>>();
        public OnlineUserTrackerService()
        {
            
        }
        public int GetOnlineUserCount()
        {
            return  _users.Count;
        }

        public void UserConnected(Guid userId, string connectionId)
        {
            _users.AddOrUpdate(userId.ToString(), new HashSet<string> { connectionId },
            (key, existing) =>
            {
                existing.Add(connectionId);
                return existing;
            });
        }

        public void UserDisconnected(string connectionId)
        {
            foreach (var user in _users)
            {
                if (user.Value.Contains(connectionId))
                {
                    user.Value.Remove(connectionId);

                    if (user.Value.Count == 0)
                    {
                        _users.TryRemove(user.Key, out _);
                    }
                    break;
                }
            }
        }
    }
}