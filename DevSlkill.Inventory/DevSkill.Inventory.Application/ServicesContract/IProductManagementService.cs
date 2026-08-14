using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IProductManagementService
    {
        Task CreateProduct(Product product);
        Task DeleteProductAsync(Guid id);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<(IList<Product> data, int total, int totalDisplay)> GetProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task<(IList<ProductDto> data, int total, int totalDisplay)> GetProductsSpAsync(int pageIndex, int pageSize, ProductSearchDto search, string? order);
        Task UpdateProductAsync(Product product);
        Task<IList<Product>> SearchProductsByNameAsync(string searchTerm);
        public Task<IEnumerable<Product>> GetAllProductByWarehouseAsync(Guid warehouseId);
        public Task<IList<Product>> GetAllProductAsync();
        public Task<int> GetTotalProductCount();
        //Task<bool> HasStockAdjustmentsAsync(Guid productId);
    }
}
