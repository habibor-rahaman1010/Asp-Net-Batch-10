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
    public class BarcodeTypeManagementService : IBarcodeTypeManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;

        public BarcodeTypeManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }
        public async Task<IList<BarcodeType>> GetBarCodeTypes()
        {
            return await _inventoryUnitOfWork.BarcodeTypeRepository.GetAllAsync();
        }
    }
}
