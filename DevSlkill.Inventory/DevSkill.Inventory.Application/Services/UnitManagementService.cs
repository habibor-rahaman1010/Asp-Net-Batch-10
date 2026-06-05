using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Services
{
    public class UnitManagementService : IUnitManagementService
    {
        private readonly IInventoryUnitOfWork _unitMeasurementUnitOfWork;

        public UnitManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _unitMeasurementUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddUnitAsync(Unit unit)
        {
            await _unitMeasurementUnitOfWork.UnitRepository.AddAsync(unit);
            await _unitMeasurementUnitOfWork.SaveAsync();
        }

        public async Task DeleteUnitAsync(Guid id)
        {
            await _unitMeasurementUnitOfWork.UnitRepository.RemoveAsync(id);
            await _unitMeasurementUnitOfWork.SaveAsync();
        }

        public async Task<IList<Unit>> GetAllUnitAsync()
        {
            return await _unitMeasurementUnitOfWork.UnitRepository.GetAllAsync();
        }

        public Task<(IList<Unit> data, int total, int totalDisplay)> GetAllUnitAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return _unitMeasurementUnitOfWork.UnitRepository.GetPagedUnitsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<Unit> GetUnitByIdAsync(Guid id)
        {
            return await _unitMeasurementUnitOfWork.UnitRepository.GetByIdAsync(id);
        }

        public async Task UpdateUnitAsync(Unit unit)
        {
            await _unitMeasurementUnitOfWork.UnitRepository.EditAsync(unit);
            await _unitMeasurementUnitOfWork.SaveAsync();
        }
    }
}
