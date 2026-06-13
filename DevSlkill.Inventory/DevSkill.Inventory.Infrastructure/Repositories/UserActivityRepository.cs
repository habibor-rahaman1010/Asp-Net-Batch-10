using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class UserActivityRepository : Repository<UserActivity, Guid>, IUserActivityRepository
    {
        private readonly InventoryDbContext _dbContext;

        public UserActivityRepository(InventoryDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<int> CalculateBounceRate()
        {
            try
            {
                var totalUsers = await _dbContext.UserActivities
                    .Select(x => x.UserId)
                    .Distinct()
                    .CountAsync();

                if (totalUsers == 0)
                {
                    return 0;
                }

                var bouncedUsers = await _dbContext.UserActivities
                    .Where(x => x.PageVisited <= 5)
                    .Select(x => x.UserId)
                    .Distinct()
                    .CountAsync();

                return (int)Math.Round((decimal)bouncedUsers / totalUsers * 100, 2);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<int> CalculateEngagementRateAsync()
        {
            try
            {
                var totalUsers = await _dbContext.UserActivities
                    .Select(x => x.UserId)
                    .Distinct()
                    .CountAsync();

                if (totalUsers == 0)
                {
                    return 0;
                }

                var engagedUsers = await _dbContext.UserActivities
                    .Where(x => x.PageVisited > 5)
                    .Select(x => x.UserId)
                    .Distinct()
                    .CountAsync();

                return (int)Math.Round((decimal)engagedUsers / totalUsers * 100, 2);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<int> GetActiveUserCountAsync()
        {
            try
            {
                var activeThreshold = DateTime.Now.AddMinutes(-15);

                return await _dbContext.UserActivities 
                    .CountAsync(x => x.LastActivityTime >= activeThreshold);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<int> GetUniqueVisitors()
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                var uniqueVisitors = await _dbContext.UserActivityLogs
                    .Where(x => x.VisitTime >= today && x.VisitTime < tomorrow)
                    .Select(x => x.UserId)
                    .Distinct()
                    .CountAsync();

                return uniqueVisitors;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
