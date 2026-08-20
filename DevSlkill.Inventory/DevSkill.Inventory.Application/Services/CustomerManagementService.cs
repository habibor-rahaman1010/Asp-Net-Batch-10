using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class CustomerManagementService : ICustomerManagementService
    {
        private readonly IInventoryUnitOfWork _customerUnitOfWork;

        public CustomerManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _customerUnitOfWork = unitOfWork;
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            if (await _customerUnitOfWork.CustomerRepository.IsCustomerCodeDuplicateAsync(customer.CustomerCode))
            {
                throw new InvalidOperationException("Customer code should be unique!");
            }

            customer.Created = DateTime.Now;
            customer.Updated = DateTime.Now;

            // The opening balance is where the running due starts from.
            customer.CurrentOutstanding = customer.OpeningBalance;

            await _customerUnitOfWork.CustomerRepository.AddAsync(customer);
            await _customerUnitOfWork.SaveAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if (await _customerUnitOfWork.CustomerRepository.IsCustomerCodeDuplicateAsync(customer.CustomerCode, customer.Id))
            {
                throw new InvalidOperationException("Customer code should be unique!");
            }

            var existingCustomer = await _customerUnitOfWork.CustomerRepository.GetByIdAsync(customer.Id)
                ?? throw new InvalidOperationException("Customer not found.");

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerCode = customer.CustomerCode;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;
            existingCustomer.CustomerType = customer.CustomerType;
            existingCustomer.CreditLimit = customer.CreditLimit;
            existingCustomer.Status = customer.Status;
            existingCustomer.Updated = DateTime.Now;

            // Opening balance shifts the running due by the same amount, otherwise an
            // edit would silently drop already recorded sales and payments.
            if (existingCustomer.OpeningBalance != customer.OpeningBalance)
            {
                existingCustomer.CurrentOutstanding += customer.OpeningBalance - existingCustomer.OpeningBalance;
                existingCustomer.OpeningBalance = customer.OpeningBalance;
            }

            await _customerUnitOfWork.CustomerRepository.EditAsync(existingCustomer);
            await _customerUnitOfWork.SaveAsync();
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            await _customerUnitOfWork.CustomerRepository.RemoveAsync(id);
            await _customerUnitOfWork.SaveAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _customerUnitOfWork.CustomerRepository.GetByIdAsync(id);
        }

        public async Task<IList<Customer>> GetAllCustomerAsync()
        {
            return await _customerUnitOfWork.CustomerRepository.GetAllAsync();
        }

        public async Task<IList<Customer>> GetActiveCustomerAsync()
        {
            return await _customerUnitOfWork.CustomerRepository.GetActiveCustomersAsync();
        }

        public async Task<IList<Customer>> SearchCustomersByNameAsync(string searchTerm)
        {
            return await _customerUnitOfWork.CustomerRepository.SearchCustomersByNameAsync(searchTerm);
        }

        public async Task<(IList<Customer> data, int total, int totalDisplay)> GetCustomersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _customerUnitOfWork.CustomerRepository.GetPagedCustomersAsync(pageIndex, pageSize, search, order);
        }

        public async Task ToggleCustomerStatusAsync(Guid id)
        {
            var customer = await _customerUnitOfWork.CustomerRepository.GetByIdAsync(id)
                ?? throw new InvalidOperationException("Customer not found.");

            customer.Status = customer.Status == CustomerStatus.Active
                ? CustomerStatus.Inactive
                : CustomerStatus.Active;

            customer.Updated = DateTime.Now;

            await _customerUnitOfWork.CustomerRepository.EditAsync(customer);
            await _customerUnitOfWork.SaveAsync();
        }

        public async Task<int> GetTotalCustomerCount()
        {
            return await _customerUnitOfWork.CustomerRepository.GetCountAsync();
        }

        public async Task<string> GenerateCustomerCodeAsync()
        {
            var count = await _customerUnitOfWork.CustomerRepository.GetCountAsync();

            string code;
            do
            {
                count++;
                code = $"CUS-{count:D5}";
            }
            while (await _customerUnitOfWork.CustomerRepository.IsCustomerCodeDuplicateAsync(code));

            return code;
        }
    }
}
