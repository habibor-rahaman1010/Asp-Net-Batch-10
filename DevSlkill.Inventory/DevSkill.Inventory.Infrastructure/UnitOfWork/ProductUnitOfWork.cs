using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using DevSkill.Inventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.UnitOfWork
{
    public class ProductUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }

        public ProductUnitOfWork(InventoryDbContext productDbContext, 
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository) : base(productDbContext) 
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
        }

        public async Task<(IList<ProductDto> data, int total, int totalDisplay)> GetPagedProductUsingSPAsync(int pageIndex, int pageSize, ProductSearchDto search, string? order)
        {
            var procedureName = "ProductAdvancedSearch";
            var result = await SqlUtility.QueryWithStoredProcedureAsync<ProductDto>(procedureName, new Dictionary<string, object>
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "OrderBy", order },
                { "ProductName", string.IsNullOrEmpty(search.ProductName) ? null : search.ProductName },
                { "ProductType", string.IsNullOrEmpty(search.ProductType.ToString()) ? null : (int)search.ProductType },
                { "CategoryId", string.IsNullOrEmpty(search.CategoryId) ? null : Guid.Parse(search.CategoryId) }

            },
            new Dictionary<string, Type>
            {
                { "Total", typeof(int) },
                { "TotalDisplay", typeof(int) },
            });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
    }
}
     