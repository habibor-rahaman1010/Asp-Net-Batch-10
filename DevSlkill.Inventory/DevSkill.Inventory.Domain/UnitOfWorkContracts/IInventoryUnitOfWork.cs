using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.UnitOfWorkContracts
{
    public interface IInventoryUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IBarcodeTypeRepository BarcodeTypeRepository { get; }
        IUnitRepository UnitRepository { get; }
        IBrandRepository BrandRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IBusinessLocationRepository BusinessLocationRepository { get; }
        IWarrantyRepository WarrantyRepository { get; }
        IApplicableTaxRepository ApplicableTaxRepository { get; }
        ISellingPriceTaxRepository SellingPriceTaxRepository { get; }
        IAdjustmentTypeRepository AdjustmentTypeRepository { get; }
        IStockAdjustmentRepository StockAdjustmentRepository { get; }
        IUserActivityRepository UserActivityRepository { get; }
        IStockTransferRepository StockTransferRepository { get;}
        IStockTransferItemRepository StockTransferItemRepository { get; }
        Task<(IList<ProductDto> data, int total, int totalDisplay)> GetPagedProductUsingSPAsync(int pageIndex,
            int pageSize, ProductSearchDto search, string? order);
    }
}
