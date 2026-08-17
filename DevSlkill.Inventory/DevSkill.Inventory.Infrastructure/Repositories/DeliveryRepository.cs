using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class DeliveryRepository : Repository<Delivery, Guid>, IDeliveryRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public DeliveryRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<Delivery> data, int total, int totalDisplay)> GetPagedDeliveriesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.BusinessLocation)
                              .Include(n => n.ProformaInvoice)
                              .Include(n => n.SalesOrder),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.DeliveryNo.Contains(search.Value) ||
                             x.ProformaInvoice!.ProformaNo.Contains(search.Value) ||
                             x.SalesOrder!.SalesOrderNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.BusinessLocation)
                              .Include(n => n.ProformaInvoice)
                              .Include(n => n.SalesOrder),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<Delivery?> GetDeliveryByIdAsync(Guid id)
        {
            return await _inventoryDbContext.Deliveries
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.ProformaInvoice!)
                    .ThenInclude(x => x.ProformaInvoiceItems!)
                        .ThenInclude(x => x.Product)
                .Include(x => x.SalesOrder!)
                    .ThenInclude(x => x.SalesOrderItems!)
                        .ThenInclude(x => x.Product)
                .Include(x => x.DeliveryItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Every shipment already written against one proforma invoice. The service
        /// adds these up to know what is still owed to the customer.
        /// </summary>
        public async Task<IList<Delivery>> GetDeliveriesByProformaInvoiceAsync(Guid proformaInvoiceId)
        {
            return await _inventoryDbContext.Deliveries
                .Include(x => x.DeliveryItems)
                .Where(x => x.ProformaInvoiceId == proformaInvoiceId)
                .ToListAsync();
        }

        /// <summary>
        /// Every shipment already written against one sales order, read the same way
        /// as the proforma side so both sources answer the question identically.
        /// </summary>
        public async Task<IList<Delivery>> GetDeliveriesBySalesOrderAsync(Guid salesOrderId)
        {
            return await _inventoryDbContext.Deliveries
                .Include(x => x.DeliveryItems)
                .Where(x => x.SalesOrderId == salesOrderId)
                .ToListAsync();
        }

        public async Task<bool> IsDeliveryNoDuplicateAsync(string deliveryNo)
        {
            return await GetCountAsync(x => x.DeliveryNo == deliveryNo) > 0;
        }
    }
}
