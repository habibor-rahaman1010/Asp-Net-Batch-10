using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    /// <summary>
    /// Holding stock for a confirmed sale. Physical stock never moves here; only
    /// <see cref="Domain.Entities.Product.ReservedStock"/> does, and only inside a
    /// transaction.
    /// </summary>
    public interface IStockReservationManagementService
    {
        /// <summary>
        /// Writes the hold. When <paramref name="reserveImmediately"/> is set the stock
        /// is held in the very same transaction.
        /// </summary>
        Task<Guid> CreateStockReservationAsync(StockReservation stockReservation, bool reserveImmediately);

        Task<bool> UpdateStockReservationAsync(StockReservation stockReservation);
        Task<bool> DeleteStockReservationAsync(Guid id);

        /// <summary>Puts the hold on: the reserved column goes up and free stock goes down.</summary>
        Task<bool> ConfirmStockReservationAsync(Guid id);

        /// <summary>Gives the hold back on purpose, because the sale is no longer expected.</summary>
        Task<bool> ReleaseStockReservationAsync(Guid id);

        Task<bool> CancelStockReservationAsync(Guid id);

        Task<StockReservation?> GetStockReservationByIdAsync(Guid id);
        Task<(IList<StockReservation> data, int total, int totalDisplay)> GetStockReservationsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<string> GenerateReservationNoAsync();

        /// <summary>
        /// The lines of a sales order with everything the reservation screen needs.
        /// <paramref name="excludeReservationId"/> leaves the reservation being edited
        /// out of the already held totals, otherwise it would block its own quantity.
        /// </summary>
        Task<IList<ReservableLineDto>> GetReservableLinesAsync(Guid salesOrderId, Guid? excludeReservationId = null);

        /// <summary>Sales orders a reservation may be raised against.</summary>
        Task<IList<SalesOrder>> GetReservableSalesOrdersAsync();

        /// <summary>Active holds that have run past their expiry date, for the review screen.</summary>
        Task<IList<StockReservation>> GetExpiredStockReservationsAsync();
    }
}
