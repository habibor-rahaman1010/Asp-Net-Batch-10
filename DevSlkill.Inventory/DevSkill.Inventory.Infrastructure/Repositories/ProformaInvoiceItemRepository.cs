using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProformaInvoiceItemRepository : Repository<ProformaInvoiceItem, Guid>, IProformaInvoiceItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public ProformaInvoiceItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public Task RemoveRangeAsync(IList<ProformaInvoiceItem> proformaInvoiceItems)
        {
            try
            {
                _inventoryDbContext.ProformaInvoiceItems.RemoveRange(proformaInvoiceItems);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
