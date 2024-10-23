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
        private readonly IInventoryUnitOfWork _applicableTaxUnitOfWork;

        public ApplicableTaxManagementService(IInventoryUnitOfWork applicableTaxUnitOfWork)
        {
            _applicableTaxUnitOfWork = applicableTaxUnitOfWork;
        }

        public async Task<IList<ApplicableTax>> GetAllApplicableTax()
        {
            return await _applicableTaxUnitOfWork.ApplicableTaxRepository.GetAllAsync();
        }

        public async Task<ApplicableTax> GetApplicableTaxByIdAsync(Guid id)
        {
            return await _applicableTaxUnitOfWork.ApplicableTaxRepository.GetByIdAsync(id);
        }
    }
}
