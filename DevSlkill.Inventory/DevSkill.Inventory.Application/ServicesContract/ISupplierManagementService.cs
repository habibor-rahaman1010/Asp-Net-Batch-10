using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISupplierManagementService
    {
        Task CreateSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Guid id);
        Task<Supplier?> GetSupplierByIdAsync(Guid id);
        Task<IList<Supplier>> GetAllSupplierAsync();
        Task<IList<Supplier>> GetActiveSupplierAsync();
        Task<IList<Supplier>> SearchSuppliersByNameAsync(string searchTerm);
        Task<(IList<Supplier> data, int total, int totalDisplay)> GetSuppliersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task ToggleSupplierStatusAsync(Guid id);
        Task<string> GenerateSupplierCodeAsync();
    }
}
