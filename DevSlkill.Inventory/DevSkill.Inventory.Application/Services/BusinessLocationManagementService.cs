using DevSkill.Inventory.Application.ServicesContract;
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
        public readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public BusinessLocationManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<IList<BusinessLocation>> GetAllBusinessLocationAsync()
        {
            return await _inventoryUnitOfWork.BusinessLocationRepository.GetAllAsync();
        }
    }
}
