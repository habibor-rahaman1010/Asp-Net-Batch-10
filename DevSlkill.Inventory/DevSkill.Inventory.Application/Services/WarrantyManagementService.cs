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
    public class WarrantyManagementService : IWarrantyManagementService
    {
        private readonly IInventoryUnitOfWork _warrantyUnitOfWork;
        public WarrantyManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _warrantyUnitOfWork = inventoryUnitOfWork;
        }

        public async Task AddWarrantyAsync(Warranty warranty)
        {
            await _warrantyUnitOfWork.WarrantyRepository.AddAsync(warranty);
            await _warrantyUnitOfWork.SaveAsync();
        }

        public async Task DeleteWarrantyAsync(Guid id)
        {
            await _warrantyUnitOfWork.WarrantyRepository.RemoveAsync(id);
            await _warrantyUnitOfWork.SaveAsync();
        }

        public async Task<IList<Warranty>> GetAllWarrantyAsync()
        {
            return await _warrantyUnitOfWork.WarrantyRepository.GetAllAsync();
        }

        public Task<(IList<Warranty> data, int total, int totalDisplay)> GetAllWarrantyAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return _warrantyUnitOfWork.WarrantyRepository.GetPagedWarrantiesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<Warranty> GetWarrantyByIdAsync(Guid id)
        {
            return await _warrantyUnitOfWork.WarrantyRepository.GetByIdAsync(id);
        }

        public async Task UpdateWarrantyAsync(Warranty warranty)
        {
            await _warrantyUnitOfWork.WarrantyRepository.EditAsync(warranty);
            await _warrantyUnitOfWork.SaveAsync();
        }
    }
}
