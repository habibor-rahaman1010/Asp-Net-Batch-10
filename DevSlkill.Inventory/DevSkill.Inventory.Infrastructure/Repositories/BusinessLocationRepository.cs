using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class BusinessLocationRepository : Repository<BusinessLocation, Guid>, IBusinessLocationRepository
    {
        public BusinessLocationRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<(IList<BusinessLocation> data, int total, int totalDisplay)> GetPagedBusinessLocationAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.LocationName.Contains(search.Value) || x.City.Contains(search.Value) || x.Country.Contains(search.Value), order, null, pageIndex, pageSize, true);
            }
        }
    }
}
