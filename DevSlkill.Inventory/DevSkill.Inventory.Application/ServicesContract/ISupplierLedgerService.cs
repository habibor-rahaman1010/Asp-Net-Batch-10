using DevSkill.Inventory.Domain.Dtos;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISupplierLedgerService
    {
        /// <summary>
        /// Rebuilds one supplier's account from the documents themselves rather than
        /// reading the stored running total, so the statement adds up on its own.
        /// </summary>
        Task<SupplierLedgerDto> GetSupplierLedgerAsync(Guid supplierId, DateTime fromDate, DateTime toDate);
    }
}
