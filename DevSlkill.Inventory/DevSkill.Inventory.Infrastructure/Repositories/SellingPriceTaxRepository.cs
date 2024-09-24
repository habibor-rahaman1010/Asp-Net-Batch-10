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
    public class SellingPriceTaxRepository : Repository<SellingPriceTax, Guid>, ISellingPriceTaxRepository
    {
        public SellingPriceTaxRepository(InventoryDbContext context) : base(context)
        {
        }
    }
}
