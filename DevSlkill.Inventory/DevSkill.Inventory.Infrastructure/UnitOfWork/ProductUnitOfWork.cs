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
    public class ProductUnitOfWork : UnitOfWork, IProductUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }

        public ProductUnitOfWork(ProductDbContext productDbContext, IProductRepository productRepository) : base(productDbContext) 
        {
            ProductRepository = productRepository;
        }
    }
}
