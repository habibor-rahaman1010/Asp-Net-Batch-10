using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IUserActivityRepository : IRepositoryBase<UserActivity, Guid>
    {
        public Task<int> CalculateBounceRate();
        public Task<int> CalculateEngagementRateAsync();
        public Task<int> GetActiveUserCountAsync();
        public Task<int> GetUniqueVisitors();
    }
}
