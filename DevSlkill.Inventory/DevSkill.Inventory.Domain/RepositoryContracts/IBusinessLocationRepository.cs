using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IBusinessLocationRepository : IRepositoryBase<BusinessLocation, Guid>
    {
        Task<(IList<BusinessLocation> data, int total, int totalDisplay)> GetPagedBusinessLocationAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);

        /// <summary>
        /// How much of the catalogue each warehouse keeps, busiest first. A warehouse
        /// holding nothing comes back as a zero rather than being left out, so the
        /// list is every warehouse there is.
        /// </summary>
        Task<IList<WarehouseStockPointDto>> GetWarehouseProductCountsAsync();
    }
}
