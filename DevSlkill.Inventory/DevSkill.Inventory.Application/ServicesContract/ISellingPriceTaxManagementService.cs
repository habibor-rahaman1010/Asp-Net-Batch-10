using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISellingPriceTaxManagementService
    {
        Task<IList<SellingPriceTax>> GetAllSellingPriceTax();
        Task<SellingPriceTax> GetSellingPriceTaxByIdAsync(Guid id);
        Task<(IList<SellingPriceTax> data, int total, int totalDisplay)> GetAllSellingPriceTaxAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddSellingPriceTaxAsync(SellingPriceTax sellingPriceTax);
        Task UpdateSellingPriceTaxAsync(SellingPriceTax sellingPriceTax);
        Task DeleteSellingPriceTaxAsync(Guid id);
    }
}
