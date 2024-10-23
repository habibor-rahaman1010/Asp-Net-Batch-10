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
        private readonly IInventoryUnitOfWork _barcodeTypeUnitOfWork;

        public BarcodeTypeManagementService(IInventoryUnitOfWork barcodeTypeUnitOfWork)
        {
            _barcodeTypeUnitOfWork = barcodeTypeUnitOfWork;
        }

        public async Task<BarcodeType> GetBarcodeTypeId(Guid id)
        {
            return await _barcodeTypeUnitOfWork.BarcodeTypeRepository.GetByIdAsync(id);
        }

        public async Task<IList<BarcodeType>> GetBarCodeTypes()
        {
            return await _barcodeTypeUnitOfWork.BarcodeTypeRepository.GetAllAsync();
        }
    }
}
