using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface IStockReservationRepository : IRepositoryBase<StockReservation, Guid>
    {
        Task<(IList<StockReservation> data, int total, int totalDisplay)> GetPagedStockReservationsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<StockReservation?> GetStockReservationByIdAsync(Guid id);

        /// <summary>
        /// Every reservation written against one sales order, whatever its state. The
        /// reservation screen works out what is still holdable from these.
        /// </summary>
        Task<IList<StockReservation>> GetStockReservationsBySalesOrderAsync(Guid salesOrderId);

        Task<IList<StockReservation>> GetStockReservationsByStatusAsync(params StockReservationStatus[] statuses);

        /// <summary>Active holds whose expiry date has already passed.</summary>
        Task<IList<StockReservation>> GetExpiredStockReservationsAsync(DateTime asOfDate);

        Task<bool> IsReservationNoDuplicateAsync(string reservationNo);
    }
}
