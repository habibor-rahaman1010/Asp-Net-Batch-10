using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProductRepository : IRepositoryBase<Product, Guid>
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<bool> IsTitleDuplicateAsync(string productName, Guid? id = null);
        Task<IList<Product>> SearchProductsByNameAsync(string searchTerm);
    }
}
