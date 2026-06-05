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
    public interface IStockAdjustmentManagementService
    {
        Task<(IList<StockAdjustment> data, int total, int totalDisplay)> GetAllStockAdjustmentAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddStockAdjustmentAsync(StockAdjustment stockAdjustment);
        Task DeleteStockAdjustmentAsync(Guid id);
        Task<StockAdjustment> GetStockAdjustmentyByIdAsync(Guid id); 
    }
}
