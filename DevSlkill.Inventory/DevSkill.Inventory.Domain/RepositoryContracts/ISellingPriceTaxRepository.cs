using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISellingPriceTaxRepository : IRepositoryBase<SellingPriceTax, Guid>
    {
        Task<(IList<SellingPriceTax> data, int total, int totalDisplay)> GetPagedSellingPriceTaxesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
