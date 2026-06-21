using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockTransferRepository : Repository<StockTransfer, Guid>, IStockTransferRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public StockTransferRepository(InventoryDbContext context) : base(context)
        {
            _inventoryDbContext = context;
        }
    }
}
