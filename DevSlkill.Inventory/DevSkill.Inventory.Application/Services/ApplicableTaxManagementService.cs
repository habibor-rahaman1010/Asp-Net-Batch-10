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
    public class ApplicableTaxManagementService : IApplicableTaxManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;

        public ApplicableTaxManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<IList<ApplicableTax>> GetAllApplicablTax()
        {
            return await _inventoryUnitOfWork.ApplicableTaxRepository.GetAllAsync();
        }

        public async Task<ApplicableTax> GetApplicableTaxByIdAsync(Guid id)
        {
            return await _inventoryUnitOfWork.ApplicableTaxRepository.GetByIdAsync(id);
        }
    }
}
