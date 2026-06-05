using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductTypeRepository : Repository<ProductType, Guid>, IProductTypeRepository
    {
        public ProductTypeRepository(InventoryDbContext inventoryDbcontext) : base(inventoryDbcontext)
        {
        }

        public async Task<(IList<ProductType> data, int total, int totalDisplay)> GetPagedProductTypesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.ProductTypeName.Contains(search.Value) || x.Description.Contains(search.Value), order, null, pageIndex, pageSize, true);
            }
        }
    }
}
