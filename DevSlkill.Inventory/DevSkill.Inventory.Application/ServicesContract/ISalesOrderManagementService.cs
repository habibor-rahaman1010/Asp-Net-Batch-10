using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ISalesOrderManagementService
    {
        Task<Guid> CreateSalesOrderAsync(SalesOrder salesOrder);
        Task<bool> UpdateSalesOrderAsync(SalesOrder salesOrder);
        Task<bool> DeleteSalesOrderAsync(Guid id);
        Task<SalesOrder?> GetSalesOrderByIdAsync(Guid id);
        Task<(IList<SalesOrder> data, int total, int totalDisplay)> GetSalesOrdersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<IList<SalesOrder>> GetSalesOrdersByStatusAsync(params SalesOrderStatus[] statuses);
        Task<string> GenerateSalesOrderNoAsync();

        /// <summary>
        /// Commits to the sale. It is what allows stock to be reserved and goods to be
        /// delivered, and it is where the customer's credit limit is checked.
        /// </summary>
        Task ConfirmSalesOrderAsync(Guid id);

        /// <summary>
        /// Withdraws the order. Anything already delivered has to be returned first,
        /// and every hold it still has is released.
        /// </summary>
        Task CancelSalesOrderAsync(Guid id);

        /// <summary>
        /// Finishes an order short of its ordered quantity, for when the customer will
        /// take no more. Any remaining hold is released.
        /// </summary>
        Task CloseSalesOrderAsync(Guid id);

        /// <summary>
        /// Live preview of one order line, run through the very same server side
        /// arithmetic used when the order is saved.
        /// </summary>
        Task<SalesLinePreviewDto?> GetLinePreviewAsync(Guid productId, decimal quantity,
            decimal unitPrice, decimal discountAmount);
    }
}
