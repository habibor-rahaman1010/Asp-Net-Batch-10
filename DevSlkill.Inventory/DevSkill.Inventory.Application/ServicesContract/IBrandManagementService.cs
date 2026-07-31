using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;


namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IBrandManagementService
    {
        public Task<IList<Brand>> GetAllBrandAsync();
        public Task<Brand> GetBrandByIdAsync(Guid id);
        public Task<(IList<Brand> data, int total, int totalDisplay)> GetBrandsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        public Task AddBrandAsync(Brand brand);
        public Task DeleteBrandAsync(Guid id);
        public Task UpdateBrandAsync(Brand brand);
        public Task<int> GetTotalBrandCount();
    }
}
