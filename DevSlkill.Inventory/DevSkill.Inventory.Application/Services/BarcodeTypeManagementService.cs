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
    public class BarcodeTypeManagementService : IBarcodeTypeManagementService
    {
        private readonly IInventoryUnitOfWork _barcodeTypeUnitOfWork;

        public BarcodeTypeManagementService(IInventoryUnitOfWork barcodeTypeUnitOfWork)
        {
            _barcodeTypeUnitOfWork = barcodeTypeUnitOfWork;
        }

        public async Task AddBarcodeTypeAsync(BarcodeType barcodeType)
        {
            await _barcodeTypeUnitOfWork.BarcodeTypeRepository.AddAsync(barcodeType);
            await _barcodeTypeUnitOfWork.SaveAsync();  
        }

        public async Task DeleteBarcodeTypeAsync(Guid id)
        {
            await _barcodeTypeUnitOfWork.BarcodeTypeRepository.RemoveAsync(id);
            await _barcodeTypeUnitOfWork.SaveAsync();
        }

        public async Task<(IList<BarcodeType> data, int total, int totalDisplay)> GetAllBarcodeTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _barcodeTypeUnitOfWork.BarcodeTypeRepository.GetPagedBarcodeTypesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<BarcodeType> GetBarcodeTypeId(Guid id)
        {
            return await _barcodeTypeUnitOfWork.BarcodeTypeRepository.GetByIdAsync(id);
        }

        public async Task<IList<BarcodeType>> GetBarCodeTypes()
        {
            return await _barcodeTypeUnitOfWork.BarcodeTypeRepository.GetAllAsync();
        }

        public async Task UpdateBarcodeTypeAsync(BarcodeType barcodeType)
        {
            await _barcodeTypeUnitOfWork.BarcodeTypeRepository.EditAsync(barcodeType);
            await _barcodeTypeUnitOfWork.SaveAsync();
        }
    }
}
