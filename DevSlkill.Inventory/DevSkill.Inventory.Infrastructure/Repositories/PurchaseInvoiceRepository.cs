using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PurchaseInvoiceRepository : Repository<PurchaseInvoice, Guid>, IPurchaseInvoiceRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PurchaseInvoiceRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<PurchaseInvoice> data, int total, int totalDisplay)> GetPagedPurchaseInvoicesAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Supplier).Include(n => n.PurchaseOrder),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.InvoiceNo.Contains(search.Value) ||
                             x.SupplierInvoiceNo.Contains(search.Value) ||
                             x.PurchaseOrder!.PurchaseOrderNo.Contains(search.Value) ||
                             x.Supplier!.SupplierName.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Supplier).Include(n => n.PurchaseOrder),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseInvoice?> GetPurchaseInvoiceByIdAsync(Guid id)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.GoodsReceipt)
                .Include(x => x.PaymentTerm)
                .Include(x => x.PurchaseInvoiceItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByPurchaseOrderAsync(Guid purchaseOrderId)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Include(x => x.PurchaseInvoiceItems!)
                    .ThenInclude(x => x.Product)
                .Where(x => x.PurchaseOrderId == purchaseOrderId)
                .ToListAsync();
        }

        public async Task<IList<PurchaseInvoice>> GetPurchaseInvoicesBySupplierAsync(Guid supplierId)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Include(x => x.PurchaseOrder)
                .Where(x => x.SupplierId == supplierId)
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByStatusAsync(params PurchaseInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseOrder)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Include(x => x.Supplier)
                .Include(x => x.PurchaseOrder)
                    .ThenInclude(x => x!.BusinessLocation)
                .Include(x => x.PurchaseInvoiceItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .Where(x => x.InvoiceDate >= fromDate && x.InvoiceDate <= toDate)
                .OrderBy(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<IList<MonthlyAmountDto>> GetMonthlyInvoicedTotalsAsync(DateTime fromDate, DateTime toDate,
            params PurchaseInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Where(x => x.InvoiceDate >= fromDate
                         && x.InvoiceDate <= toDate
                         && statuses.Contains(x.Status))
                .GroupBy(x => new { x.InvoiceDate.Year, x.InvoiceDate.Month })
                .Select(g => new MonthlyAmountDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(x => x.GrandTotal)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();
        }

        public async Task<IList<SupplierPurchasePointDto>> GetSupplierInvoicedTotalsAsync(DateTime fromDate,
            DateTime toDate, params PurchaseInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Where(x => x.InvoiceDate >= fromDate
                         && x.InvoiceDate <= toDate
                         && statuses.Contains(x.Status))
                // Grouped on the key rather than the name, so two suppliers who happen
                // to share a name are never added up into one bar.
                .GroupBy(x => new
                {
                    x.SupplierId,
                    Name = x.Supplier!.SupplierName,
                    Code = x.Supplier!.SupplierCode
                })
                .Select(g => new SupplierPurchasePointDto
                {
                    SupplierName = g.Key.Name,
                    SupplierCode = g.Key.Code,
                    PurchasedAmount = g.Sum(x => x.GrandTotal),
                    InvoiceCount = g.Count()
                })
                .OrderByDescending(x => x.PurchasedAmount)
                .ToListAsync();
        }

        public async Task<DateTime?> GetEarliestInvoiceDateAsync(params PurchaseInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.PurchaseInvoices
                .Where(x => statuses.Contains(x.Status))
                .OrderBy(x => x.InvoiceDate)
                .Select(x => (DateTime?)x.InvoiceDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsInvoiceNoDuplicateAsync(string invoiceNo)
        {
            return await GetCountAsync(x => x.InvoiceNo == invoiceNo) > 0;
        }

        /// <summary>
        /// One supplier may not bill the same reference twice, but two suppliers are
        /// free to use the same numbering of their own.
        /// </summary>
        public async Task<bool> IsSupplierInvoiceNoDuplicateAsync(Guid supplierId, string supplierInvoiceNo, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(supplierInvoiceNo))
            {
                return false;
            }

            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value
                                             && x.SupplierId == supplierId
                                             && x.SupplierInvoiceNo == supplierInvoiceNo
                                             && x.Status != PurchaseInvoiceStatus.Cancelled) > 0;
            }

            return await GetCountAsync(x => x.SupplierId == supplierId
                                         && x.SupplierInvoiceNo == supplierInvoiceNo
                                         && x.Status != PurchaseInvoiceStatus.Cancelled) > 0;
        }
    }
}
