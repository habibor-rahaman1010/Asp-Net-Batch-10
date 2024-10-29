using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IApplicableTaxManagementService
    {
        Task<IList<ApplicableTax>> GetAllApplicableTax();
        Task<ApplicableTax> GetApplicableTaxByIdAsync(Guid id);
        Task<(IList<ApplicableTax> data, int total, int totalDisplay)> GetAllApplicableTaxAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddApplicableTaxAsync(ApplicableTax applicableTax);
        Task UpdateApplicableTaxAsync(ApplicableTax applicableTax);
        Task DeleteApplicableTaxAsync(Guid id);
    }
}
