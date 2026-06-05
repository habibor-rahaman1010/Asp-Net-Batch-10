using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBusinessLocationRepository : IRepositoryBase<BusinessLocation, Guid>
    {
        Task<(IList<BusinessLocation> data, int total, int totalDisplay)> GetPagedBusinessLocationAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
