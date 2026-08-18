using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public ProductRepository(InventoryDbContext inventoryDbcontext) : base(inventoryDbcontext)
        {
            _inventoryDbContext = inventoryDbcontext;
        }

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
                .Include(z => z.SellingPriceTax!)
            );

            return product.FirstOrDefault()!;
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            if (string.IsNullOrWhiteSpace(search.Value))
            {
                return await GetDynamicAsync(null, order, y => y.Include(z => z.Category).Include(n => n.Brand)!, pageIndex, pageSize, true);
            }
            else
            {
                return await GetDynamicAsync(x => x.ProductName.Contains(search.Value) || x.Description.Contains(search.Value), order, y => y.Include(z => z.Category).Include(n => n.Brand)!, pageIndex, pageSize, true);
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

        public async Task<IList<Product>> SearchProductsByNameAsync(string searchTerm)
        {
            return await GetAsync(p => p.ProductName.Contains(searchTerm), null);
        }

        public async Task<IEnumerable<Product>> GetAllProductByWarehouseAsync(Guid warehouseId)
        {
            try
            {
                var products = await GetAsync(x => x.BusinessLocationId == warehouseId, null);
                return products ?? Enumerable.Empty<Product>();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<(int unitsOnHand, int unitsReserved)> GetStockUnitTotalsAsync()
        {
            // One round trip, two sums. Reading the rows back only to add them up would
            // grow with the catalogue for no gain.
            var totals = await _inventoryDbContext.Products
                .GroupBy(x => 1)
                .Select(g => new
                {
                    OnHand = g.Sum(x => x.CurrentStock),
                    Reserved = g.Sum(x => x.ReservedStock)
                })
                .FirstOrDefaultAsync();

            // No products at all leaves nothing to group, so the empty shelf is zero.
            return (totals?.OnHand ?? 0, totals?.Reserved ?? 0);
        }
    }
}