using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.UnitOfWork
{
    public class InventoryUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductTypeRepository ProductTypeRepository { get; private set; }
        public IBarcodeTypeRepository BarcodeTypeRepository {  get; private set; }
        public IUnitRepository UnitRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }
        public IBusinessLocationRepository BusinessLocationRepository { get; private set; }
        public IWarrantyRepository WarrantyRepository { get; private set; }
        public IApplicableTaxRepository ApplicableTaxRepository { get; private set; }
        public ISellingPriceTaxRepository SellingPriceTaxRepository { get; private set; }
        public IAdjustmentTypeRepository AdjustmentTypeRepository { get; private set; }
        public IStockAdjustmentRepository StockAdjustmentRepository { get; private set; }
        public IUserActivityRepository UserActivityRepository { get; private set; }

        public InventoryUnitOfWork(InventoryDbContext productDbContext,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductTypeRepository productTypeRepository,
            IBarcodeTypeRepository barcodeTypeRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository,
            ISubCategoryRepository subCategoryRepository, 
            IWarrantyRepository warrantyRepository,
            IBusinessLocationRepository businessLocationRepository, 
            IApplicableTaxRepository applicableTaxRepository,
            ISellingPriceTaxRepository sellingPriceTaxRepository,
            IAdjustmentTypeRepository adjustmentTypeRepository,
            IStockAdjustmentRepository stockAdjustmentRepository,
            IUserActivityRepository userActivityRepository)
            : base(productDbContext)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            ProductTypeRepository = productTypeRepository;
            BarcodeTypeRepository = barcodeTypeRepository;
            UnitRepository = unitRepository;
            BrandRepository = brandRepository;
            SubCategoryRepository = subCategoryRepository;
            BusinessLocationRepository = businessLocationRepository;
            WarrantyRepository = warrantyRepository;
            ApplicableTaxRepository = applicableTaxRepository;
            SellingPriceTaxRepository = sellingPriceTaxRepository;
            AdjustmentTypeRepository = adjustmentTypeRepository;
            StockAdjustmentRepository = stockAdjustmentRepository;
            UserActivityRepository = userActivityRepository;
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
                { "CategoryId", string.IsNullOrEmpty(search.CategoryId) ? null : Guid.Parse(search.CategoryId) },
                { "BrandId", string.IsNullOrEmpty(search.BrandId) ? null : Guid.Parse(search.BrandId)},
                { "UnitId", string.IsNullOrEmpty(search.UnitId) ? null : Guid.Parse(search.UnitId)},
                { "BusinessLocationId", string.IsNullOrEmpty(search.BusinessLocationId) ? null : Guid.Parse(search.BusinessLocationId)},
                { "SellingPriceTaxId", string.IsNullOrEmpty(search.SellingPriceTaxId) ? null : Guid.Parse(search.SellingPriceTaxId)},
                { "ApplicableTaxId", string.IsNullOrEmpty(search.ApplicableTaxId) ? null : Guid.Parse(search.ApplicableTaxId)},
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
     