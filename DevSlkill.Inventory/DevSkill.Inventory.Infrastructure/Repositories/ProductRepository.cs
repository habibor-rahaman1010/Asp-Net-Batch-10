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
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(InventoryDbContext inventoryDbcontext) : base(inventoryDbcontext) 
        {

        }

       /* public async Task<Product> GetProductByIdAsync(Guid id)
        {
            *//*return (await GetAsync(x => x.Id == id, y => y.Include(z => z.Category))).FirstOrDefault();*//*
            var product = await GetAsync(x => x.Id == id, y => y.Include(z => z.Category));
            return product.FirstOrDefault();
        }*/

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await GetAsync(x => x.Id == id, y => y
                .Include(z => z.BarcodeType)
                .Include(z => z.Category)
                .Include(z => z.Brand)
                .Include(z => z.Unit)
                .Include(z => z.Subcategory)
                .Include(z => z.BusinessLocation)
                .Include(z => z.Warranty)
                .Include(z => z.ProductType)
                .Include(z => z.ApplicableTax)
                .Include(z => z.SellingPriceTax)
            );

            return product.FirstOrDefault();
        }


        public async Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, y => y.Include(z => z.Category).Include(n => n.Brand), pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.ProductName.Contains(search.Value) || x.Description.Contains(search.Value), order, y => y.Include(z => z.Category).Include(n => n.Brand), pageIndex, pageSize, true);
            }
        }

        public async Task<bool> IsTitleDuplicateAsync(string productName, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.ProductName == productName) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.ProductName == productName) > 0;
            }
        }
    }
}
