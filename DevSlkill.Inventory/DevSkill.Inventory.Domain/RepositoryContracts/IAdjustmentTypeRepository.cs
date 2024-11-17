using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IAdjustmentTypeRepository : IRepositoryBase<AdjustmentType, Guid>
    {
        Task<(IList<AdjustmentType> data, int total, int totalDisplay)> GetPagedAdjustmentTypesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
