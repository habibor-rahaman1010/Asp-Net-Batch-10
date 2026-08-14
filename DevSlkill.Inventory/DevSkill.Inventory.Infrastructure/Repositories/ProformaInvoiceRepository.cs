using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProformaInvoiceRepository : Repository<ProformaInvoice, Guid>, IProformaInvoiceRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public ProformaInvoiceRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<ProformaInvoice> data, int total, int totalDisplay)> GetPagedProformaInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer).Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.ProformaNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer).Include(n => n.BusinessLocation),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<ProformaInvoice?> GetProformaInvoiceByIdAsync(Guid id)
        {
            return await _inventoryDbContext.ProformaInvoices
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.PaymentTerm)
                .Include(x => x.ProformaInvoiceItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Used by the flows that only work on documents in a certain state, the
        /// delivery screen being the first of them.
        /// </summary>
        public async Task<IList<ProformaInvoice>> GetProformaInvoicesByStatusAsync(params ProformaInvoiceStatus[] statuses)
        {
            return await _inventoryDbContext.ProformaInvoices
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.ProformaDate)
                .ToListAsync();
        }

        public async Task<bool> IsProformaNoDuplicateAsync(string proformaNo)
        {
            return await GetCountAsync(x => x.ProformaNo == proformaNo) > 0;
        }
    }
}
