using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CreditNoteRepository : Repository<CreditNote, Guid>, ICreditNoteRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public CreditNoteRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<CreditNote> data, int total, int totalDisplay)> GetPagedCreditNotesAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.SalesReturn)
                              .Include(n => n.SalesInvoice),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.CreditNoteNo.Contains(search.Value) ||
                             x.SalesReturn!.ReturnNo.Contains(search.Value) ||
                             x.SalesInvoice!.InvoiceNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.SalesReturn)
                              .Include(n => n.SalesInvoice),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<CreditNote?> GetCreditNoteByIdAsync(Guid id)
        {
            return await _inventoryDbContext.CreditNotes
                .Include(x => x.Customer)
                .Include(x => x.SalesReturn)
                .Include(x => x.SalesInvoice)
                .Include(x => x.CreditNoteItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<CreditNote>> GetCreditNotesByCustomerAsync(Guid customerId)
        {
            return await _inventoryDbContext.CreditNotes
                .Include(x => x.SalesReturn)
                .Include(x => x.SalesInvoice)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.CreditNoteDate)
                .ToListAsync();
        }

        public async Task<IList<CreditNote>> GetCreditNotesByStatusAsync(params CreditNoteStatus[] statuses)
        {
            return await _inventoryDbContext.CreditNotes
                .Include(x => x.Customer)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.CreditNoteDate)
                .ToListAsync();
        }

        public async Task<CreditNote?> GetCreditNoteBySalesReturnAsync(Guid salesReturnId)
        {
            return await _inventoryDbContext.CreditNotes
                .Include(x => x.CreditNoteItems)
                .FirstOrDefaultAsync(x => x.SalesReturnId == salesReturnId);
        }

        public async Task<IList<CreditNote>> GetCreditNotesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _inventoryDbContext.CreditNotes
                .Include(x => x.Customer)
                .Include(x => x.CreditNoteItems)
                .Where(x => x.CreditNoteDate >= fromDate && x.CreditNoteDate <= toDate)
                .OrderBy(x => x.CreditNoteDate)
                .ToListAsync();
        }

        public async Task<bool> IsCreditNoteNoDuplicateAsync(string creditNoteNo)
        {
            return await GetCountAsync(x => x.CreditNoteNo == creditNoteNo) > 0;
        }
    }
}
