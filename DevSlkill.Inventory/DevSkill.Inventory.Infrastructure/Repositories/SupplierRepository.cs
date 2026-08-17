using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SupplierRepository : Repository<Supplier, Guid>, ISupplierRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SupplierRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<Supplier> data, int total, int totalDisplay)> GetPagedSuppliersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.PaymentTerm), pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.SupplierName.Contains(search.Value) ||
                             x.SupplierCode.Contains(search.Value) ||
                             x.ContactPerson.Contains(search.Value) ||
                             x.Phone.Contains(search.Value) ||
                             x.Email.Contains(search.Value),
                        order,
                        x => x.Include(n => n.PaymentTerm),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> IsSupplierCodeDuplicateAsync(string supplierCode, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.SupplierCode == supplierCode) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.SupplierCode == supplierCode) > 0;
            }
        }

        /// <summary>
        /// Only these may be picked on a new purchase document. A blacklisted
        /// supplier stays out even though its history is kept.
        /// </summary>
        public async Task<IList<Supplier>> GetActiveSuppliersAsync()
        {
            return await GetAsync(x => x.Status == SupplierStatus.Active,
                y => y.OrderBy(z => z.SupplierName), null, true);
        }

        public async Task<IList<Supplier>> SearchSuppliersByNameAsync(string searchTerm)
        {
            return await GetAsync(x => x.SupplierName.Contains(searchTerm) ||
                                       x.SupplierCode.Contains(searchTerm), null);
        }

        public async Task<Supplier?> GetSupplierByIdAsync(Guid id)
        {
            return await _inventoryDbContext.Suppliers
                .Include(x => x.PaymentTerm)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
