using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<bool> IsTitleDuplicateAsync(string productName, Guid? id = null);
        Task<IList<Product>> SearchProductsByNameAsync(string searchTerm);
        public Task<IEnumerable<Product>> GetAllProductByWarehouseAsync(Guid warehouseId);

        /// <summary>
        /// Physical and reserved units across the whole catalogue, added up in the
        /// database. The dashboard only needs the two totals, never the rows.
        /// </summary>
        Task<(int unitsOnHand, int unitsReserved)> GetStockUnitTotalsAsync();
    }
}
