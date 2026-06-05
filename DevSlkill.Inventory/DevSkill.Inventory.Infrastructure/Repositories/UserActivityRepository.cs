using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class UserActivityRepository : Repository<UserActivity, Guid>, IUserActivityRepository
    {
        public UserActivityRepository(InventoryDbContext context) : base(context)
        {

        }
    }
}
