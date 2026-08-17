using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class GoodsReceiptItemRepository : Repository<GoodsReceiptItem, Guid>, IGoodsReceiptItemRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public GoodsReceiptItemRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<IList<GoodsReceiptItem>> GetItemsByGoodsReceiptAsync(Guid goodsReceiptId)
        {
            return await _inventoryDbContext.GoodsReceiptItems
                .Include(x => x.Product)
                    .ThenInclude(x => x!.Unit)
                .Where(x => x.GoodsReceiptId == goodsReceiptId)
                .ToListAsync();
        }

        public Task RemoveRangeAsync(IList<GoodsReceiptItem> items)
        {
            try
            {
                _inventoryDbContext.GoodsReceiptItems.RemoveRange(items);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }
    }
}
