using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPurchaseReturnManagementService
    {
        Task<Guid> CreatePurchaseReturnAsync(PurchaseReturn purchaseReturn);
        Task<bool> UpdatePurchaseReturnAsync(PurchaseReturn purchaseReturn);
        Task<bool> DeletePurchaseReturnAsync(Guid id);
        Task<PurchaseReturn?> GetPurchaseReturnByIdAsync(Guid id);
        Task<(IList<PurchaseReturn> data, int total, int totalDisplay)> GetPurchaseReturnsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GenerateReturnNoAsync();

        /// <summary>
        /// Every return raised against one goods receipt, cancelled ones included.
        /// The callers decide which of them still count.
        /// </summary>
        Task<IList<PurchaseReturn>> GetPurchaseReturnsByGoodsReceiptAsync(Guid goodsReceiptId);

        /// <summary>
        /// The receipts a return may be raised against, which is only the ones that
        /// actually brought goods in.
        /// </summary>
        Task<IList<GoodsReceipt>> GetReturnableGoodsReceiptsAsync(params GoodsReceiptStatus[] statuses);

        /// <summary>
        /// Sends the goods back. This is the only place the purchase module decreases
        /// stock, and it happens inside one transaction with the receipt update.
        /// </summary>
        Task ConfirmPurchaseReturnAsync(Guid id);

        /// <summary>
        /// Reverses a return. A confirmed one brings its stock straight back in.
        /// </summary>
        Task CancelPurchaseReturnAsync(Guid id);

        /// <summary>
        /// The lines of a goods receipt that may still be sent back.
        /// <paramref name="excludePurchaseReturnId"/> lets an edit measure against the
        /// right figure by ignoring what that same return is already holding.
        /// </summary>
        Task<IList<ReturnableLineDto>> GetReturnableLinesAsync(Guid goodsReceiptId,
            Guid? excludePurchaseReturnId = null);
    }
}
