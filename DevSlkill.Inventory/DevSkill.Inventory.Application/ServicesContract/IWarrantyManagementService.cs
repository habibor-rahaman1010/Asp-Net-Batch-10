using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IWarrantyManagementService
    {
        Task<IList<Warranty>> GetAllWarrantyAsync();
        Task<Warranty> GetWarrantyByIdAsync(Guid id);
        Task<(IList<Warranty> data, int total, int totalDisplay)> GetAllWarrantyAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task DeleteWarrantyAsync(Guid id);
     /*   Task AddWarrantyAsync(Warranty brand);
        Task UpdateWarrantyAsync(Warranty brand);*/
    }
}
