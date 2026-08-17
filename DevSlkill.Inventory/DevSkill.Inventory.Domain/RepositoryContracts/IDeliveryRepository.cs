using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IDeliveryRepository : IRepositoryBase<Delivery, Guid>
    {
        Task<(IList<Delivery> data, int total, int totalDisplay)> GetPagedDeliveriesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<Delivery?> GetDeliveryByIdAsync(Guid id);
        Task<IList<Delivery>> GetDeliveriesByProformaInvoiceAsync(Guid proformaInvoiceId);

        /// <summary>
        /// Every shipment already written against one sales order. The service adds
        /// these up to know what is still owed to the customer.
        /// </summary>
        Task<IList<Delivery>> GetDeliveriesBySalesOrderAsync(Guid salesOrderId);

        Task<bool> IsDeliveryNoDuplicateAsync(string deliveryNo);
    }
}
