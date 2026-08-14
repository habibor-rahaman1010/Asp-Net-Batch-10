using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IPriceListManagementService
    {
        Task<bool> CreatePriceListAsync(PriceList priceList);
        Task<bool> UpdatePriceListAsync(PriceList priceList);
        Task<bool> DeletePriceListAsync(Guid id);
        Task<PriceList?> GetPriceListByIdAsync(Guid id);
        Task<(IList<PriceList> data, int total, int totalDisplay)> GetPriceListsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);

        /// <summary>
        /// Authoritative selling price of a product for a customer type. Falls back to
        /// the product's own selling price when no price list covers it.
        /// </summary>
        Task<decimal> GetEffectiveUnitPriceAsync(Guid productId, CustomerType customerType,
            decimal quantity, DateTime onDate);
    }
}
