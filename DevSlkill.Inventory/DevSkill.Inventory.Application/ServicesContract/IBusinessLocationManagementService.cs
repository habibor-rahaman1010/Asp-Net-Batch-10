using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;


namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IBusinessLocationManagementService
    {
        public Task<IList<BusinessLocation>> GetAllBusinessLocationAsync();
        public Task<BusinessLocation> GetBusinessLocationByIdAsync(Guid id);
        public Task<(IList<BusinessLocation> data, int total, int totalDisplay)> GetAllBusinessLocationAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        public Task AddBusinessLocationAsync(BusinessLocation businessLocation);
        public Task DeleteBusinessLocationAsync(Guid id);
        public Task UpdateBusinessLocationAsync(BusinessLocation warranty);
        public Task<int> GetTotalWarehouseCount();
    }
}
