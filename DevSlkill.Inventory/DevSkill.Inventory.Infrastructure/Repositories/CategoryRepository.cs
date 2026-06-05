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
    public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(InventoryDbContext inventoryDbcontext) : base(inventoryDbcontext)
        {
        }

        public async Task<(IList<Category> data, int total, int totalDisplay)> GetPagedCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.CategoryName.Contains(search.Value) || x.Description.Contains(search.Value), order, null, pageIndex, pageSize, true);
            }
        }
    }
}
