using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using DevSkill.Inventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.UnitOfWork
{
    public class ProductUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }

        public ProductUnitOfWork(InventoryDbContext productDbContext, IProductRepository productRepository) : base(productDbContext) 
        {
            ProductRepository = productRepository;
        }
    }
}
