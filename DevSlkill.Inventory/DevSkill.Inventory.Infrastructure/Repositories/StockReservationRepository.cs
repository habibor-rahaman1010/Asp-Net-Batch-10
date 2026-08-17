using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class StockReservationRepository : Repository<StockReservation, Guid>, IStockReservationRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public StockReservationRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<(IList<StockReservation> data, int total, int totalDisplay)> GetPagedStockReservationsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(search.Value))
                {
                    return await GetDynamicAsync(null, order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.BusinessLocation)
                              .Include(n => n.SalesOrder),
                        pageIndex, pageSize, true);
                }
                else
                {
                    return await GetDynamicAsync(
                        x => x.ReservationNo.Contains(search.Value) ||
                             x.SalesOrder!.SalesOrderNo.Contains(search.Value) ||
                             x.Customer!.CustomerName.Contains(search.Value) ||
                             x.Customer!.CustomerCode.Contains(search.Value),
                        order,
                        x => x.Include(n => n.Customer)
                              .Include(n => n.BusinessLocation)
                              .Include(n => n.SalesOrder),
                        pageIndex, pageSize, true);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<StockReservation?> GetStockReservationByIdAsync(Guid id)
        {
            return await _inventoryDbContext.StockReservations
                .Include(x => x.Customer)
                .Include(x => x.BusinessLocation)
                .Include(x => x.SalesOrder!)
                    .ThenInclude(x => x.SalesOrderItems!)
                        .ThenInclude(x => x.Product)
                .Include(x => x.StockReservationItems!)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x!.Unit)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<StockReservation>> GetStockReservationsBySalesOrderAsync(Guid salesOrderId)
        {
            return await _inventoryDbContext.StockReservations
                .Include(x => x.StockReservationItems)
                .Where(x => x.SalesOrderId == salesOrderId)
                .ToListAsync();
        }

        public async Task<IList<StockReservation>> GetStockReservationsByStatusAsync(
            params StockReservationStatus[] statuses)
        {
            return await _inventoryDbContext.StockReservations
                .Include(x => x.Customer)
                .Include(x => x.SalesOrder)
                .Include(x => x.StockReservationItems)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.ReservationDate)
                .ToListAsync();
        }

        /// <summary>
        /// Active holds that have run past their expiry date. They still hold stock
        /// until someone releases them, which is deliberate: expiry is reported, never
        /// applied behind the user's back.
        /// </summary>
        public async Task<IList<StockReservation>> GetExpiredStockReservationsAsync(DateTime asOfDate)
        {
            return await _inventoryDbContext.StockReservations
                .Include(x => x.Customer)
                .Include(x => x.SalesOrder)
                .Include(x => x.StockReservationItems)
                .Where(x => x.Status == StockReservationStatus.Reserved && x.ExpiryDate < asOfDate.Date)
                .OrderBy(x => x.ExpiryDate)
                .ToListAsync();
        }

        public async Task<bool> IsReservationNoDuplicateAsync(string reservationNo)
        {
            return await GetCountAsync(x => x.ReservationNo == reservationNo) > 0;
        }
    }
}
