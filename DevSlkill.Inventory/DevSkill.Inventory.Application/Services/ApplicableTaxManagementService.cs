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
    public class ApplicableTaxManagementService : IApplicableTaxManagementService
    {
        private readonly IInventoryUnitOfWork _applicableTaxUnitOfWork;

        public ApplicableTaxManagementService(IInventoryUnitOfWork applicableTaxUnitOfWork)
        {
            _applicableTaxUnitOfWork = applicableTaxUnitOfWork;
        }

        public async Task AddApplicableTaxAsync(ApplicableTax applicableTax)
        {
            await _applicableTaxUnitOfWork.ApplicableTaxRepository.AddAsync(applicableTax);
            await _applicableTaxUnitOfWork.SaveAsync();
        }

        public async Task DeleteApplicableTaxAsync(Guid id)
        {
            await _applicableTaxUnitOfWork.ApplicableTaxRepository.RemoveAsync(id);
            await _applicableTaxUnitOfWork.SaveAsync();
        }

        public async Task<IList<ApplicableTax>> GetAllApplicableTax()
        {
            return await _applicableTaxUnitOfWork.ApplicableTaxRepository.GetAllAsync();
        }

        public async Task<(IList<ApplicableTax> data, int total, int totalDisplay)> GetAllApplicableTaxAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _applicableTaxUnitOfWork.ApplicableTaxRepository.GetPagedApplicableTaxesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<ApplicableTax> GetApplicableTaxByIdAsync(Guid id)
        {
            return await _applicableTaxUnitOfWork.ApplicableTaxRepository.GetByIdAsync(id);
        }

        public async Task UpdateApplicableTaxAsync(ApplicableTax applicableTax)
        {
            await _applicableTaxUnitOfWork.ApplicableTaxRepository.EditAsync(applicableTax);
            await _applicableTaxUnitOfWork.SaveAsync();
        }
    }
}
