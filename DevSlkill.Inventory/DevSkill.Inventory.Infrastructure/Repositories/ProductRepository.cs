using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(InventoryDbContext productDbContext) : base(productDbContext) 
        {
        }

        public (IList<Product> data, int total, int totalDisplay) GetPagedProducts(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return GetDynamic(null, order, null, pageIndex, pageSize, true);
            }
            else
            {
                return GetDynamic(x => x.ProductName.Contains(search.Value), order, null, pageIndex, pageSize, true);
            }
        }
    }
}
