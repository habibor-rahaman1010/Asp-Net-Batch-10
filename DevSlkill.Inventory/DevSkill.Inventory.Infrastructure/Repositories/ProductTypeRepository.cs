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
    }
}
