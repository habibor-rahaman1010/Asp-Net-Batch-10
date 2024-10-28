using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBarcodeTypeRepository : IRepositoryBase<BarcodeType, Guid>
    {
        Task<(IList<BarcodeType> data, int total, int totalDisplay)> GetPagedBarcodeTypesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
