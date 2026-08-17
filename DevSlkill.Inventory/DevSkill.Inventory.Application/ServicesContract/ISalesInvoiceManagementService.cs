using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISalesInvoiceManagementService
    {
        Task<Guid> CreateSalesInvoiceAsync(SalesInvoice salesInvoice, bool postImmediately);
        Task<bool> UpdateSalesInvoiceAsync(SalesInvoice salesInvoice);
        Task<bool> DeleteSalesInvoiceAsync(Guid id);

        /// <summary>
        /// Claims the money: the customer's outstanding balance goes up, the order's
        /// invoiced quantity moves and the salesperson earns their commission.
        /// </summary>
        Task<bool> PostSalesInvoiceAsync(Guid id);

        /// <summary>Takes a posted invoice back off the customer account and off the commission.</summary>
        Task<bool> CancelSalesInvoiceAsync(Guid id);

        Task<SalesInvoice?> GetSalesInvoiceByIdAsync(Guid id);
        Task<(IList<SalesInvoice> data, int total, int totalDisplay)> GetSalesInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<SalesInvoice>> GetSalesInvoicesByStatusAsync(params SalesInvoiceStatus[] statuses);
        Task<string> GenerateInvoiceNoAsync();

        /// <summary>
        /// The lines of a sales order with everything the invoice screen needs. Only
        /// delivered goods may be billed, so the delivered quantity is the ceiling.
        /// <paramref name="excludeInvoiceId"/> leaves the invoice being edited out of
        /// the already billed totals.
        /// </summary>
        Task<IList<BillableSalesLineDto>> GetBillableLinesAsync(Guid salesOrderId, Guid? excludeInvoiceId = null);

        /// <summary>Sales orders an invoice may be raised against.</summary>
        Task<IList<SalesOrder>> GetBillableSalesOrdersAsync();

        /// <summary>Confirmed shipments of one order, so an invoice can name a single one.</summary>
        Task<IList<Delivery>> GetInvoiceableDeliveriesAsync(Guid salesOrderId);
    }
}
