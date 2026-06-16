using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IAdjustmentTypeManagementService
    {
        public Task<IList<AdjustmentType>> GetAllAdjustmentTypeAsync();
        public Task<(IList<AdjustmentType> data, int total, int totalDisplay)> GetAllAdjustmentTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        public Task AddAdjustmentTypeAsync(AdjustmentType adjustmentType);
        public Task DeleteAdjustmentTypeAsync(Guid id);
        public Task UpdateAdjustmentTypeAsync(AdjustmentType adjustmentType);
        public Task<AdjustmentType> GetAdjustmentTypeByIdAsync(Guid id);
    }
}
