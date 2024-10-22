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
    public class BusinessLocationManagementService : IBusinessLocationManagementService
    {
        public readonly IInventoryUnitOfWork _businessLocationUnitOfWork;
        public BusinessLocationManagementService(IInventoryUnitOfWork businessLocationUnitOfWork)
        {
            _businessLocationUnitOfWork = businessLocationUnitOfWork;
        }

        public async Task AddBusinessLocationAsync(BusinessLocation businessLocation)
        {
            await _businessLocationUnitOfWork.BusinessLocationRepository.AddAsync(businessLocation);
            await _businessLocationUnitOfWork.SaveAsync();
        }

        public async Task DeleteBusinessLocationAsync(Guid id)
        {
            await _businessLocationUnitOfWork.BusinessLocationRepository.RemoveAsync(id);
            await _businessLocationUnitOfWork.SaveAsync();
        }

        public async Task<IList<BusinessLocation>> GetAllBusinessLocationAsync()
        {
            return await _businessLocationUnitOfWork.BusinessLocationRepository.GetAllAsync();
        }

        public async Task<(IList<BusinessLocation> data, int total, int totalDisplay)> GetAllBusinessLocationAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _businessLocationUnitOfWork.BusinessLocationRepository.GetPagedBusinessLocationAsync(pageIndex, pageSize, search, order);
        }

        public async Task<BusinessLocation> GetBusinessLocationByIdAsync(Guid id)
        {
            return await _businessLocationUnitOfWork.BusinessLocationRepository.GetByIdAsync(id);
        }

        public async Task UpdateBusinessLocationAsync(BusinessLocation warranty)
        {
            await _businessLocationUnitOfWork.BusinessLocationRepository.EditAsync(warranty);
            await _businessLocationUnitOfWork.SaveAsync();
        }
    }
}
