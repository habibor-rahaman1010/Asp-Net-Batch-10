using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IAdjustmentTypeManagementService
    {
        Task<(IList<AdjustmentType> data, int total, int totalDisplay)> GetAllAdjustmentTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddAdjustmentTypeAsync(AdjustmentType adjustmentType);
        Task DeleteAdjustmentTypeAsync(Guid id);
        Task UpdateAdjustmentTypeAsync(AdjustmentType adjustmentType);
        Task<AdjustmentType> GetAdjustmentTypeByIdAsync(Guid id);

        /*Task<IList<Warranty>> GetAllWarrantyAsync();*/
    }
}
