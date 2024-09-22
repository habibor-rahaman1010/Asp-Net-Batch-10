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

        public IProductTypeRepository ProductTypeRepository { get; private set; }

        public IBarcodeTypeRepository BarcodeTypeRepository {  get; private set; }

        public ProductUnitOfWork(InventoryDbContext productDbContext, 
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IProductTypeRepository productTypeRepository,
            IBarcodeTypeRepository barcodeTypeRepository)
            : base(productDbContext) 
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            ProductTypeRepository = productTypeRepository;
            BarcodeTypeRepository = barcodeTypeRepository;
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
                { "ProductTypeId", string.IsNullOrEmpty(search.ProductTypeId) ? null : Guid.Parse(search.ProductTypeId) },
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
     