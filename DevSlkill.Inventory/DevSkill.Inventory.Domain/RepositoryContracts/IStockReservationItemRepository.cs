using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockReservationItemRepository : IRepositoryBase<StockReservationItem, Guid>
    {
        Task<IList<StockReservationItem>> GetItemsByStockReservationAsync(Guid stockReservationId);
        Task RemoveRangeAsync(IList<StockReservationItem> items);
    }
}
