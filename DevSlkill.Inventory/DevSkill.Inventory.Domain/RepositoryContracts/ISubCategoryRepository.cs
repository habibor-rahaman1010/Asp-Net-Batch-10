using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISubCategoryRepository : IRepositoryBase<SubCategory, Guid>
    {
        Task<(IList<SubCategory> data, int total, int totalDisplay)> GetPagedSubCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
