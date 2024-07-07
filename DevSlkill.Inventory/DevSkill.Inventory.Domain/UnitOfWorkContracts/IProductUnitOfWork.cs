using DevSkill.Inventory.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.UnitOfWorkContracts
{
    public interface IProductUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
    }
}
