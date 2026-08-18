using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
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
        private readonly InventoryDbContext _inventoryDbContext;

        public BusinessLocationRepository(InventoryDbContext context) : base(context)
        {
            _inventoryDbContext = context;
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

        public async Task<IList<WarehouseStockPointDto>> GetWarehouseProductCountsAsync()
        {
            // Counted from the warehouse side rather than from the products, so a
            // warehouse nobody has stored anything in still comes back, as a zero.
            return await _inventoryDbContext.BusinessLocations
                .Select(location => new WarehouseStockPointDto
                {
                    WarehouseName = location.LocationName,
                    ProductCount = _inventoryDbContext.Products
                        .Count(product => product.BusinessLocationId == location.Id),
                    UnitsOnHand = _inventoryDbContext.Products
                        .Where(product => product.BusinessLocationId == location.Id)
                        .Sum(product => (int?)product.CurrentStock) ?? 0
                })
                .OrderByDescending(x => x.ProductCount)
                .ThenBy(x => x.WarehouseName)
                .ToListAsync();
        }
    }
}
