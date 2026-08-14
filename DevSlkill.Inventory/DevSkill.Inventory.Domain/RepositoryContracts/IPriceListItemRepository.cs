using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPriceListItemRepository : IRepositoryBase<PriceListItem, Guid>
    {
        Task RemoveRangeAsync(IList<PriceListItem> priceListItems);
    }
}
