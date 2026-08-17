using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalespersonRepository : Repository<Salesperson, Guid>, ISalespersonRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public SalespersonRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<Salesperson> data, int total, int totalDisplay)> GetPagedSalespersonsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.SalespersonName.Contains(search.Value) ||
                             x.SalespersonCode.Contains(search.Value) ||
                             x.Phone.Contains(search.Value) ||
                             x.Email.Contains(search.Value),
                        order, null, pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<Salesperson?> GetSalespersonByIdAsync(Guid id)
        {
            return await _inventoryDbContext.Salespersons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Salesperson>> GetActiveSalespersonsAsync()
        {
            return await _inventoryDbContext.Salespersons
                .Where(x => x.Status == SalespersonStatus.Active)
                .OrderBy(x => x.SalespersonName)
                .ToListAsync();
        }

        /// <summary>
        /// <paramref name="excludeId"/> leaves the salesperson being edited out, so
        /// saving them without touching the code is never read as a clash.
        /// </summary>
        public async Task<bool> IsSalespersonCodeDuplicateAsync(string salespersonCode, Guid? excludeId = null)
        {
            return await _inventoryDbContext.Salespersons
                .AnyAsync(x => x.SalespersonCode == salespersonCode
                            && (!excludeId.HasValue || x.Id != excludeId.Value));
        }
    }
}
