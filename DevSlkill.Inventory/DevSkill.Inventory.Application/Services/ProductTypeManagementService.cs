using DevSkill.Inventory.Application.ServicesContract;
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

        public async Task<ProductType> GetProductTypeIdAsync(Guid id)
        {
            return await _productTypeUnitOfWork.ProductTypeRepository.GetByIdAsync(id);
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            return await _productTypeUnitOfWork.ProductTypeRepository.GetAllAsync();
        }
    }
}
