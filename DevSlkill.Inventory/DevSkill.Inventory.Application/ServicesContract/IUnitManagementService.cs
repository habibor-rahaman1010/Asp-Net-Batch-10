using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IUnitManagementService
    {
        Task<IList<Unit>> GetAllUnitAsync();
        Task<Unit> GetUnitByIdAsync(Guid id);
        Task<(IList<Unit> data, int total, int totalDisplay)> GetAllUnitAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddUnitAsync(Unit unit);
  /*      Task DeleteWarrantyAsync(Guid id);
        Task UpdateWarrantyAsync(Warranty warranty);*/
    }
}
