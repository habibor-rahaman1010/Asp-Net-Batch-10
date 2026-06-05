using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IProductTypeManagementService
    {
        Task<IList<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeIdAsync(Guid id);
        Task<(IList<ProductType> data, int total, int totalDisplay)> GetAllProductTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddProductTypeAsync(ProductType productType);
        Task UpdateProductTypeAsync(ProductType productType);
        Task DeleteProductTypeAsync(Guid id);
    }
}
