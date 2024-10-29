using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISubCategoryManagementService
    {
        Task<IList<SubCategory>> GetAllSubCategoryAsync();
        Task<SubCategory> GetSubCategoryByIdAsync(Guid id);
        Task<(IList<SubCategory> data, int total, int totalDisplay)> GetAllSubCategoryAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddSubCategoryAsync(SubCategory subCategory);
        Task UpdateSubCategoryAsync(SubCategory subCategory);
        Task DeleteSubCategoryAsync(Guid id);

    }
}
