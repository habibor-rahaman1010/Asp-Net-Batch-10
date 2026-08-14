using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ICustomerManagementService
    {
        Task CreateCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<IList<Customer>> GetAllCustomerAsync();
        Task<IList<Customer>> GetActiveCustomerAsync();
        Task<IList<Customer>> SearchCustomersByNameAsync(string searchTerm);
        Task<(IList<Customer> data, int total, int totalDisplay)> GetCustomersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task ToggleCustomerStatusAsync(Guid id);
        Task<string> GenerateCustomerCodeAsync();
    }
}
