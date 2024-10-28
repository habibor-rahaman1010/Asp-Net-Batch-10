using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IApplicableTaxRepository : IRepositoryBase<ApplicableTax, Guid>
    {
        Task<(IList<ApplicableTax> data, int total, int totalDisplay)> GetPagedApplicableTaxesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
