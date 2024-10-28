using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Services
{
    public class ProductTypeManagementService : IProductTypeManagementService
    {
        private readonly IInventoryUnitOfWork _productTypeUnitOfWork;

        public ProductTypeManagementService(IInventoryUnitOfWork productTypeUnitOfWork)
        {
            _productTypeUnitOfWork = productTypeUnitOfWork;
        }

        public async Task AddProductTypeAsync(ProductType productType)
        {
            await _productTypeUnitOfWork.ProductTypeRepository.AddAsync(productType);
            await _productTypeUnitOfWork.SaveAsync();
        }

        public async Task DeleteProductTypeAsync(Guid id)
        {
            await _productTypeUnitOfWork.ProductTypeRepository.RemoveAsync(id);
            await _productTypeUnitOfWork.SaveAsync();
        }

        public async Task<(IList<ProductType> data, int total, int totalDisplay)> GetAllProductTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _productTypeUnitOfWork.ProductTypeRepository.GetPagedProductTypesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<ProductType> GetProductTypeIdAsync(Guid id)
        {
            return await _productTypeUnitOfWork.ProductTypeRepository.GetByIdAsync(id);
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            return await _productTypeUnitOfWork.ProductTypeRepository.GetAllAsync();
        }

        public async Task UpdateProductTypeAsync(ProductType productType)
        {
            await _productTypeUnitOfWork.ProductTypeRepository.EditAsync(productType);
            await _productTypeUnitOfWork.SaveAsync();
        }
    }
}
