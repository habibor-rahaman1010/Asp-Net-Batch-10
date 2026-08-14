using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer, Guid>
    {
        Task<(IList<Customer> data, int total, int totalDisplay)> GetPagedCustomersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<bool> IsCustomerCodeDuplicateAsync(string customerCode, Guid? id = null);
        Task<IList<Customer>> GetActiveCustomersAsync();
        Task<IList<Customer>> SearchCustomersByNameAsync(string searchTerm);
    }
}
