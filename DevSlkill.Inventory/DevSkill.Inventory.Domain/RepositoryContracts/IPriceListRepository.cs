using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IPriceListRepository : IRepositoryBase<PriceList, Guid>
    {
        Task<(IList<PriceList> data, int total, int totalDisplay)> GetPagedPriceListsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<PriceList?> GetPriceListByIdAsync(Guid id);
        Task<bool> IsPriceListNameDuplicateAsync(string priceListName, Guid? id = null);

        /// <summary>
        /// Effective price list of a customer type on the given date, items included.
        /// </summary>
        Task<PriceList?> GetEffectivePriceListAsync(CustomerType customerType, DateTime onDate);
    }
}
