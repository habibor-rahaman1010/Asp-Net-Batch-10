using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IProductManagementService
    {
        void CreateProduct(Product product);
        Task<(IList<Product> data, int total, int totalDisplay)> GetProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
