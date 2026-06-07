namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IUserActivityManagementService
    {
        public Task UpdateLogoutTimeAsync(Guid userId);
        public Task<int> CalculateBounceRate();
        public Task<int> CalculateEngagementRateAsync();
        public Task<int> GetActiveUserCountAsync();
    }
}
