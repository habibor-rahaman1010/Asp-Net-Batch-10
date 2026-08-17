using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SupplierPaymentRepository : Repository<SupplierPayment, Guid>, ISupplierPaymentRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SupplierPaymentRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<SupplierPayment> data, int total, int totalDisplay)> GetPagedSupplierPaymentsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Supplier),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.PaymentNo.Contains(search.Value) ||
                             x.ReferenceNo.Contains(search.Value) ||
                             x.Supplier!.SupplierName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Supplier),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<SupplierPayment?> GetSupplierPaymentByIdAsync(Guid id)
        {
            return await _inventoryDbContext.SupplierPayments
                .Include(x => x.Supplier)
                .Include(x => x.SupplierPaymentAllocations!)
                    .ThenInclude(x => x.PurchaseInvoice)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Every payment made to one supplier, including the cancelled ones. The
        /// callers decide which of them still count.
        /// </summary>
        public async Task<IList<SupplierPayment>> GetSupplierPaymentsBySupplierAsync(Guid supplierId)
        {
            return await _inventoryDbContext.SupplierPayments
                .Include(x => x.SupplierPaymentAllocations!)
                    .ThenInclude(x => x.PurchaseInvoice)
                .Where(x => x.SupplierId == supplierId)
                .OrderByDescending(x => x.PaymentDate)
                .ToListAsync();
        }

        public async Task<IList<SupplierPayment>> GetSupplierPaymentsByInvoiceAsync(Guid purchaseInvoiceId)
        {
            return await _inventoryDbContext.SupplierPayments
                .Include(x => x.SupplierPaymentAllocations)
                .Where(x => x.SupplierPaymentAllocations!
                    .Any(a => a.PurchaseInvoiceId == purchaseInvoiceId))
                .ToListAsync();
        }

        public async Task<bool> IsPaymentNoDuplicateAsync(string paymentNo)
        {
            return await GetCountAsync(x => x.PaymentNo == paymentNo) > 0;
        }
    }
}
