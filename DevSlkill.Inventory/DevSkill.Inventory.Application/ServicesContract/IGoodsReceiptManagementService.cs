using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IGoodsReceiptManagementService
    {
        Task<Guid> CreateGoodsReceiptAsync(GoodsReceipt goodsReceipt);
        Task<bool> UpdateGoodsReceiptAsync(GoodsReceipt goodsReceipt);
        Task<bool> DeleteGoodsReceiptAsync(Guid id);
        Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(Guid id);
        Task<(IList<GoodsReceipt> data, int total, int totalDisplay)> GetGoodsReceiptsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GenerateGoodsReceiptNoAsync();

        /// <summary>
        /// Every receipt raised against one purchase order, cancelled ones included.
        /// The callers decide which of them still count.
        /// </summary>
        Task<IList<GoodsReceipt>> GetGoodsReceiptsByPurchaseOrderAsync(Guid purchaseOrderId);

        /// <summary>
        /// Brings the goods in. This is the only place the purchase module increases
        /// stock, and it happens inside one transaction with the order update.
        /// </summary>
        Task ConfirmGoodsReceiptAsync(Guid id);

        /// <summary>
        /// Reverses a receipt. A confirmed one takes its stock straight back out.
        /// </summary>
        Task CancelGoodsReceiptAsync(Guid id);

        /// <summary>
        /// The still open lines of a purchase order, ready to be received.
        /// <paramref name="excludeGoodsReceiptId"/> lets an edit measure against the
        /// right figure by ignoring what that same receipt is already holding.
        /// </summary>
        Task<IList<ReceivableLineDto>> GetReceivableLinesAsync(Guid purchaseOrderId, Guid? excludeGoodsReceiptId = null);
    }
}
