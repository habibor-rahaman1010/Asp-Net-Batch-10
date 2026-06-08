namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IOnlineUserTrackerService
    {
        public void UserConnected(Guid userId, string connectionId);
        public void UserDisconnected(string connectionId);
        public int GetOnlineUserCount();
    }
}
