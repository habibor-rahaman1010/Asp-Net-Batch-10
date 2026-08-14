using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Data;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(InventoryDbContext inventoryDbContext) : base(inventoryDbContext)
        {
        }

        public async Task<(IList<Customer> data, int total, int totalDisplay)> GetPagedCustomersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
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
                        x => x.CustomerName.Contains(search.Value) ||
                             x.CustomerCode.Contains(search.Value) ||
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

        public async Task<bool> IsCustomerCodeDuplicateAsync(string customerCode, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id.Value && x.CustomerCode == customerCode) > 0;
            }
            else
            {
                return await GetCountAsync(x => x.CustomerCode == customerCode) > 0;
            }
        }

        public async Task<IList<Customer>> GetActiveCustomersAsync()
        {
            return await GetAsync(x => x.Status == CustomerStatus.Active,
                y => y.OrderBy(z => z.CustomerName), null, true);
        }

        public async Task<IList<Customer>> SearchCustomersByNameAsync(string searchTerm)
        {
            return await GetAsync(x => x.CustomerName.Contains(searchTerm) ||
                                       x.CustomerCode.Contains(searchTerm), null);
        }
    }
}
