using DevSkill.Inventory.Domain.Entities.PurchaseEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IGoodsReceiptItemRepository : IRepositoryBase<GoodsReceiptItem, Guid>
    {
        Task<IList<GoodsReceiptItem>> GetItemsByGoodsReceiptAsync(Guid goodsReceiptId);
        Task RemoveRangeAsync(IList<GoodsReceiptItem> items);
    }
}
