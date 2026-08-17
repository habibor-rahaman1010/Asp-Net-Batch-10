using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPurchaseInvoiceManagementService
    {
        Task<Guid> CreatePurchaseInvoiceAsync(PurchaseInvoice purchaseInvoice);
        Task<bool> UpdatePurchaseInvoiceAsync(PurchaseInvoice purchaseInvoice);
        Task<bool> DeletePurchaseInvoiceAsync(Guid id);
        Task<PurchaseInvoice?> GetPurchaseInvoiceByIdAsync(Guid id);
        Task<(IList<PurchaseInvoice> data, int total, int totalDisplay)> GetPurchaseInvoicesAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<PurchaseInvoice>> GetPurchaseInvoicesByStatusAsync(params PurchaseInvoiceStatus[] statuses);
        Task<string> GenerateInvoiceNoAsync();

        /// <summary>
        /// Puts the invoice on the supplier ledger. This is the only thing that raises
        /// the supplier's outstanding balance.
        /// </summary>
        Task PostPurchaseInvoiceAsync(Guid id);

        /// <summary>
        /// Reverses an invoice, taking a posted amount back off the supplier ledger.
        /// </summary>
        Task CancelPurchaseInvoiceAsync(Guid id);

        /// <summary>
        /// The received but not yet billed part of a purchase order.
        /// <paramref name="excludeInvoiceId"/> lets an edit measure against the right
        /// figure by ignoring what that same invoice is already holding.
        /// </summary>
        Task<IList<BillableLineDto>> GetBillableLinesAsync(Guid purchaseOrderId, Guid? excludeInvoiceId = null);

        /// <summary>
        /// Purchase order, goods receipts and purchase invoices of one order compared
        /// line by line.
        /// </summary>
        Task<ThreeWayMatchDto?> GetThreeWayMatchAsync(Guid purchaseOrderId);
    }
}
