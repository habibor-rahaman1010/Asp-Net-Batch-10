namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IUserActivityManagementService
    {
        public Task UpdateLogoutTimeAsync(Guid userId);
    }
}
