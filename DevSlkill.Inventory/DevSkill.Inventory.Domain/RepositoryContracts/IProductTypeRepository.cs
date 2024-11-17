using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IProductTypeRepository : IRepositoryBase<ProductType, Guid>
    {
        Task<(IList<ProductType> data, int total, int totalDisplay)> GetPagedProductTypesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
