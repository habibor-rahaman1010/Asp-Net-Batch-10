using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBrandRepository : IRepositoryBase<Brand, Guid>
    {
        Task<(IList<Brand> data, int total, int totalDisplay)> GetPagedBrandsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
