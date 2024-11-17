using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IBusinessLocationManagementService
    {
        Task<IList<BusinessLocation>> GetAllBusinessLocationAsync();
        Task<BusinessLocation> GetBusinessLocationByIdAsync(Guid id);
        Task<(IList<BusinessLocation> data, int total, int totalDisplay)> GetAllBusinessLocationAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddBusinessLocationAsync(BusinessLocation businessLocation);
        Task DeleteBusinessLocationAsync(Guid id);
        Task UpdateBusinessLocationAsync(BusinessLocation warranty);
    }
}
