using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IDeliveryRepository : IRepositoryBase<Delivery, Guid>
    {
        Task<(IList<Delivery> data, int total, int totalDisplay)> GetPagedDeliveriesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<Delivery?> GetDeliveryByIdAsync(Guid id);
        Task<IList<Delivery>> GetDeliveriesByProformaInvoiceAsync(Guid proformaInvoiceId);
        Task<bool> IsDeliveryNoDuplicateAsync(string deliveryNo);
    }
}
