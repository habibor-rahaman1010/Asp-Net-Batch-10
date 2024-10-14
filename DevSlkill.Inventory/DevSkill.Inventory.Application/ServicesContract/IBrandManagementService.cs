using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IBrandManagementService
    {
        Task<IList<Brand>> GetAllBrandAsync();
        Task<Brand> GetBrandByIdAsync(Guid id);
        Task<(IList<Brand> data, int total, int totalDisplay)> GetBrandsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddBrandAsync(Brand brand);
        Task DeleteBrandAsync(Guid id);

        Task UpdateBrandAsync(Brand brand);
    }
}
