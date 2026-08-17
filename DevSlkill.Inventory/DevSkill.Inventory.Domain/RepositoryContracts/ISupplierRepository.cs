using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISupplierRepository : IRepositoryBase<Supplier, Guid>
    {
        Task<(IList<Supplier> data, int total, int totalDisplay)> GetPagedSuppliersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<bool> IsSupplierCodeDuplicateAsync(string supplierCode, Guid? id = null);
        Task<IList<Supplier>> GetActiveSuppliersAsync();
        Task<IList<Supplier>> SearchSuppliersByNameAsync(string searchTerm);
        Task<Supplier?> GetSupplierByIdAsync(Guid id);
    }
}
