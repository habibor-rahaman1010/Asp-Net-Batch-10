using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class SupplierManagementService : ISupplierManagementService
    {
        private readonly IInventoryUnitOfWork _supplierUnitOfWork;

        public SupplierManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _supplierUnitOfWork = unitOfWork;
        }

        public async Task CreateSupplierAsync(Supplier supplier)
        {
            if (await _supplierUnitOfWork.SupplierRepository.IsSupplierCodeDuplicateAsync(supplier.SupplierCode))
            {
                throw new InvalidOperationException("Supplier code should be unique!");
            }

            supplier.PaymentTermId = await ResolvePaymentTermIdAsync(supplier.PaymentTermId);

            supplier.Created = DateTime.Now;
            supplier.Updated = DateTime.Now;

            // The opening balance is where the running payable starts from.
            supplier.CurrentOutstanding = supplier.OpeningBalance;

            await _supplierUnitOfWork.SupplierRepository.AddAsync(supplier);
            await _supplierUnitOfWork.SaveAsync();
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            if (await _supplierUnitOfWork.SupplierRepository.IsSupplierCodeDuplicateAsync(supplier.SupplierCode, supplier.Id))
            {
                throw new InvalidOperationException("Supplier code should be unique!");
            }

            var existingSupplier = await _supplierUnitOfWork.SupplierRepository.GetByIdAsync(supplier.Id)
                ?? throw new InvalidOperationException("Supplier not found.");

            existingSupplier.SupplierName = supplier.SupplierName;
            existingSupplier.SupplierCode = supplier.SupplierCode;
            existingSupplier.ContactPerson = supplier.ContactPerson;
            existingSupplier.Phone = supplier.Phone;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Address = supplier.Address;
            existingSupplier.TaxNumber = supplier.TaxNumber;
            existingSupplier.PaymentTermId = await ResolvePaymentTermIdAsync(supplier.PaymentTermId);
            existingSupplier.CreditLimit = supplier.CreditLimit;
            existingSupplier.Status = supplier.Status;
            existingSupplier.Updated = DateTime.Now;

            // Opening balance shifts the running payable by the same amount, otherwise
            // an edit would silently drop already recorded invoices and payments.
            if (existingSupplier.OpeningBalance != supplier.OpeningBalance)
            {
                existingSupplier.CurrentOutstanding += supplier.OpeningBalance - existingSupplier.OpeningBalance;
                existingSupplier.OpeningBalance = supplier.OpeningBalance;
            }

            await _supplierUnitOfWork.SupplierRepository.EditAsync(existingSupplier);
            await _supplierUnitOfWork.SaveAsync();
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            await _supplierUnitOfWork.SupplierRepository.RemoveAsync(id);
            await _supplierUnitOfWork.SaveAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(Guid id)
        {
            return await _supplierUnitOfWork.SupplierRepository.GetSupplierByIdAsync(id);
        }

        public async Task<IList<Supplier>> GetAllSupplierAsync()
        {
            return await _supplierUnitOfWork.SupplierRepository.GetAllAsync();
        }

        public async Task<IList<Supplier>> GetActiveSupplierAsync()
        {
            return await _supplierUnitOfWork.SupplierRepository.GetActiveSuppliersAsync();
        }

        public async Task<IList<Supplier>> SearchSuppliersByNameAsync(string searchTerm)
        {
            return await _supplierUnitOfWork.SupplierRepository.SearchSuppliersByNameAsync(searchTerm);
        }

        public async Task<(IList<Supplier> data, int total, int totalDisplay)> GetSuppliersAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _supplierUnitOfWork.SupplierRepository.GetPagedSuppliersAsync(pageIndex, pageSize, search, order);
        }

        /// <summary>
        /// Blacklisting is a deliberate decision, so the toggle only moves a supplier
        /// between active and inactive and never lifts a blacklist by accident.
        /// </summary>
        public async Task ToggleSupplierStatusAsync(Guid id)
        {
            var supplier = await _supplierUnitOfWork.SupplierRepository.GetByIdAsync(id)
                ?? throw new InvalidOperationException("Supplier not found.");

            if (supplier.Status == SupplierStatus.Blacklisted)
            {
                throw new InvalidOperationException(
                    "A blacklisted supplier can only be reinstated from the edit screen.");
            }

            supplier.Status = supplier.Status == SupplierStatus.Active
                ? SupplierStatus.Inactive
                : SupplierStatus.Active;

            supplier.Updated = DateTime.Now;

            await _supplierUnitOfWork.SupplierRepository.EditAsync(supplier);
            await _supplierUnitOfWork.SaveAsync();
        }

        public async Task<int> GetTotalSupplierCount()
        {
            return await _supplierUnitOfWork.SupplierRepository.GetCountAsync();
        }

        public async Task<string> GenerateSupplierCodeAsync()
        {
            var count = await _supplierUnitOfWork.SupplierRepository.GetCountAsync();

            string code;
            do
            {
                count++;
                code = $"SUP-{count:D5}";
            }
            while (await _supplierUnitOfWork.SupplierRepository.IsSupplierCodeDuplicateAsync(code));

            return code;
        }

        /// <summary>
        /// Payment terms stay optional, but a chosen one has to be a live row in the
        /// shared lookup table.
        /// </summary>
        private async Task<Guid?> ResolvePaymentTermIdAsync(Guid? paymentTermId)
        {
            if (!paymentTermId.HasValue || paymentTermId.Value == Guid.Empty)
            {
                return null;
            }

            var paymentTerm = await _supplierUnitOfWork.PaymentTermRepository.GetByIdAsync(paymentTermId.Value)
                ?? throw new InvalidOperationException("Payment term not found.");

            if (!paymentTerm.IsActive)
            {
                throw new InvalidOperationException($"'{paymentTerm.TermName}' is inactive and cannot be used.");
            }

            return paymentTerm.Id;
        }
    }
}
