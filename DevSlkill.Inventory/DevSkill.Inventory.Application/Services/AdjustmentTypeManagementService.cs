using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Services
{
    public class AdjustmentTypeManagementService : IAdjustmentTypeManagementService
    {
        private readonly IInventoryUnitOfWork _adjustmentTypeUnitOfWork;

        public AdjustmentTypeManagementService(IInventoryUnitOfWork adjustmentTypeUnitOfWork)
        {
            _adjustmentTypeUnitOfWork = adjustmentTypeUnitOfWork;
        }

        public async Task AddAdjustmentTypeAsync(AdjustmentType adjustmentType)
        {
            await _adjustmentTypeUnitOfWork.AdjustmentTypeRepository.AddAsync(adjustmentType);
            await _adjustmentTypeUnitOfWork.SaveAsync();
        }

        public async Task DeleteAdjustmentTypeAsync(Guid id)
        {
            await _adjustmentTypeUnitOfWork.AdjustmentTypeRepository.RemoveAsync(id);
            await _adjustmentTypeUnitOfWork.SaveAsync();
        }

        public async Task<AdjustmentType> GetAdjustmentTypeByIdAsync(Guid id)
        {
            return await _adjustmentTypeUnitOfWork.AdjustmentTypeRepository.GetByIdAsync(id);
        }

        public async Task<(IList<AdjustmentType> data, int total, int totalDisplay)> GetAllAdjustmentTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _adjustmentTypeUnitOfWork.AdjustmentTypeRepository.GetPagedAdjustmentTypesAsync(pageIndex, pageSize, search, order);
        }

        public async Task UpdateAdjustmentTypeAsync(AdjustmentType adjustmentType)
        {
            await _adjustmentTypeUnitOfWork.AdjustmentTypeRepository.EditAsync(adjustmentType);
            await _adjustmentTypeUnitOfWork.SaveAsync();
        }
    }
}
