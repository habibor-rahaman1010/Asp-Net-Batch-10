using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IInventoryUnitOfWork _productUnitOfWork;

        public ProductManagementService(IInventoryUnitOfWork productUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork;
        }

        public async Task CreateProduct(Product product)
        {
            if (!await _productUnitOfWork.ProductRepository.IsTitleDuplicateAsync(product.ProductName))
            {
                await _productUnitOfWork.ProductRepository.AddAsync(product);
                await _productUnitOfWork.SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("Product name should be unique!");
            }
        }

        public async  Task DeleteProductAsync(Guid id)
        {
            await _productUnitOfWork.ProductRepository.RemoveAsync(id);
            await _productUnitOfWork.SaveAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productUnitOfWork.ProductRepository.GetProductByIdAsync(id);
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _productUnitOfWork.ProductRepository.GetPagedProductsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<(IList<ProductDto> data, int total, int totalDisplay)> GetProductsSpAsync(int pageIndex, int pageSize, ProductSearchDto search, string? order)
        {
            return await _productUnitOfWork.GetPagedProductUsingSPAsync(pageIndex, pageSize, search, order);
        }

        public async Task<bool> HasStockAdjustmentsAsync(Guid productId)
        {
            return await _productUnitOfWork.StockAdjustmentRepository.HasStockAdjustmentsAsync(productId);
        }

        // Fetch products based on the search term
        public async Task<IList<Product>> SearchProductsByNameAsync(string searchTerm)
        {
            var products = await _productUnitOfWork.ProductRepository.SearchProductsByNameAsync(searchTerm);
            return products;
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (!await _productUnitOfWork.ProductRepository.IsTitleDuplicateAsync(product.ProductName, product.Id)) 
            {
                await _productUnitOfWork.ProductRepository.EditAsync(product);
                await _productUnitOfWork.SaveAsync();
            }
            else
            {
                throw new InvalidOperationException("Product name should be unique!");
            }
        }
    }
}
