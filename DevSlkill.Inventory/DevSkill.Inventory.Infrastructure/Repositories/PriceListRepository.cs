using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class PriceListRepository : Repository<PriceList, Guid>, IPriceListRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public PriceListRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<PriceList> data, int total, int totalDisplay)> GetPagedPriceListsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.PriceListItems)!.ThenInclude(n => n.Product),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(x => x.PriceListName.Contains(search.Value), order,
                        x => x.Include(n => n.PriceListItems)!.ThenInclude(n => n.Product),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PriceList?> GetPriceListByIdAsync(Guid id)
        {
            return await _inventoryDbContext.PriceLists
                .Include(x => x.PriceListItems!)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsPriceListNameDuplicateAsync(string priceListName, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.PriceListName == priceListName) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.PriceListName == priceListName) > 0;
            }
        }

        public async Task<PriceList?> GetEffectivePriceListAsync(CustomerType customerType, DateTime onDate)
        {
            return await _inventoryDbContext.PriceLists
                .AsNoTracking()
                .Include(x => x.PriceListItems!)
                .Where(x => x.IsActive
                            && x.CustomerType == customerType
                            && x.EffectiveFrom <= onDate
                            && (x.EffectiveTo == null || x.EffectiveTo >= onDate))
                .OrderByDescending(x => x.EffectiveFrom)
                .FirstOrDefaultAsync();
        }
    }
}
