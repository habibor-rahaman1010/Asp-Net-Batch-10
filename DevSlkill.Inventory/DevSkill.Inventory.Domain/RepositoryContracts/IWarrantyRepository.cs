using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IWarrantyRepository : IRepositoryBase<Warranty, Guid>
    {
        Task<(IList<Warranty> data, int total, int totalDisplay)> GetPagedWarrantiesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
