using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class UserActivityManagementService : IUserActivityManagementService
    {
        private readonly IInventoryUnitOfWork _userActivityUnitOfWork;

        public UserActivityManagementService(IInventoryUnitOfWork userActivityUnitOfWork)
        {
            _userActivityUnitOfWork = userActivityUnitOfWork;
        }

        public async Task UpdateLogoutTimeAsync(Guid userId)
        {
            try
            {
                var activity = await _userActivityUnitOfWork.UserActivityRepository.GetSingleAsync(x => x.UserId == userId);

                if (activity != null)
                {
                    activity.LogoutTime = DateTime.Now;
                    activity.LastActivityTime = DateTime.Now;
                    await _userActivityUnitOfWork.SaveAsync();
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
