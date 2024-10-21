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
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;

        public UnitManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddUnitAsync(Unit unit)
        {
            await _inventoryUnitOfWork.UnitRepository.AddAsync(unit);
            await _inventoryUnitOfWork.SaveAsync();
        }

        public async Task<IList<Unit>> GetAllUnitAsync()
        {
            return await _inventoryUnitOfWork.UnitRepository.GetAllAsync();
        }

        public Task<(IList<Unit> data, int total, int totalDisplay)> GetAllUnitAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return _inventoryUnitOfWork.UnitRepository.GetPagedUnitsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<Unit> GetUnitByIdAsync(Guid id)
        {
            return await _inventoryUnitOfWork.UnitRepository.GetByIdAsync(id);
        }
    }
}
