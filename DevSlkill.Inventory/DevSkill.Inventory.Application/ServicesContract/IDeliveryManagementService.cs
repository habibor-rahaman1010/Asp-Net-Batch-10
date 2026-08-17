using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IDeliveryManagementService
    {
        /// <summary>
        /// Writes the shipment. When <paramref name="confirmImmediately"/> is set the
        /// goods leave the warehouse in the very same transaction.
        /// </summary>
        /// <remarks>
        /// The shipment names either a proforma invoice or a sales order, never both
        /// and never neither.
        /// </remarks>
        Task<Guid> CreateDeliveryAsync(Delivery delivery, bool confirmImmediately);

        Task<bool> UpdateDeliveryAsync(Delivery delivery);

        Task<bool> DeleteDeliveryAsync(Guid id);

        /// <summary>Hands the goods over: stock goes out and the source document is restated.</summary>
        Task<bool> ConfirmDeliveryAsync(Guid id);

        /// <summary>Takes a confirmed shipment back: stock returns and the source is restated.</summary>
        Task<bool> CancelDeliveryAsync(Guid id);

        Task<Delivery?> GetDeliveryByIdAsync(Guid id);

        Task<(IList<Delivery> data, int total, int totalDisplay)> GetDeliveriesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);

        Task<string> GenerateDeliveryNoAsync();

        /// <summary>
        /// The lines of a source document with everything the delivery screen needs.
        /// Exactly one of the two ids is given. <paramref name="excludeDeliveryId"/>
        /// leaves the delivery being edited out of the already shipped totals,
        /// otherwise it would block its own quantity.
        /// </summary>
        Task<IList<DeliverableLineDto>> GetDeliverableLinesAsync(Guid? proformaInvoiceId, Guid? salesOrderId,
            Guid? excludeDeliveryId = null);

        /// <summary>Proforma invoices a delivery may be raised against.</summary>
        Task<IList<ProformaInvoice>> GetDeliverableProformaInvoicesAsync();

        /// <summary>Sales orders a delivery may be raised against.</summary>
        Task<IList<SalesOrder>> GetDeliverableSalesOrdersAsync();
    }
}
