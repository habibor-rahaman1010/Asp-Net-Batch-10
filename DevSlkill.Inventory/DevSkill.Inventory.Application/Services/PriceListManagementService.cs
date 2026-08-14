using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class PriceListManagementService : IPriceListManagementService
    {
        private readonly IInventoryUnitOfWork _priceListUnitOfWork;

        public PriceListManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _priceListUnitOfWork = unitOfWork;
        }

        public async Task<bool> CreatePriceListAsync(PriceList priceList)
        {
            if (await _priceListUnitOfWork.PriceListRepository.IsPriceListNameDuplicateAsync(priceList.PriceListName))
            {
                throw new InvalidOperationException("Price list name should be unique!");
            }

            await _priceListUnitOfWork.BeginTransactionAsync();

            try
            {
                priceList.Created = DateTime.Now;
                priceList.Updated = DateTime.Now;

                await _priceListUnitOfWork.PriceListRepository.AddAsync(priceList);
                await _priceListUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _priceListUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdatePriceListAsync(PriceList priceList)
        {
            if (await _priceListUnitOfWork.PriceListRepository.IsPriceListNameDuplicateAsync(priceList.PriceListName, priceList.Id))
            {
                throw new InvalidOperationException("Price list name should be unique!");
            }

            await _priceListUnitOfWork.BeginTransactionAsync();

            try
            {
                var existingPriceList = await _priceListUnitOfWork
                    .PriceListRepository
                    .GetPriceListByIdAsync(priceList.Id);

                if (existingPriceList == null)
                {
                    throw new InvalidOperationException("Price list not found.");
                }

                // Replace the whole item set, same as the stock transfer update flow.
                await _priceListUnitOfWork
                    .PriceListItemRepository
                    .RemoveRangeAsync(existingPriceList.PriceListItems?.ToList() ?? new List<PriceListItem>());

                existingPriceList.PriceListName = priceList.PriceListName;
                existingPriceList.CustomerType = priceList.CustomerType;
                existingPriceList.EffectiveFrom = priceList.EffectiveFrom;
                existingPriceList.EffectiveTo = priceList.EffectiveTo;
                existingPriceList.IsActive = priceList.IsActive;
                existingPriceList.Updated = DateTime.Now;

                foreach (var item in priceList.PriceListItems?.ToList() ?? new List<PriceListItem>())
                {
                    await _priceListUnitOfWork.PriceListItemRepository.AddAsync(new PriceListItem
                    {
                        Id = Guid.NewGuid(),
                        PriceListId = existingPriceList.Id,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        MinQuantity = item.MinQuantity
                    });
                }

                await _priceListUnitOfWork.PriceListRepository.EditAsync(existingPriceList);
                await _priceListUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _priceListUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeletePriceListAsync(Guid id)
        {
            await _priceListUnitOfWork.BeginTransactionAsync();

            try
            {
                var priceList = await _priceListUnitOfWork.PriceListRepository.GetPriceListByIdAsync(id);

                if (priceList == null)
                {
                    throw new InvalidOperationException("Price list not found.");
                }

                await _priceListUnitOfWork
                    .PriceListItemRepository
                    .RemoveRangeAsync(priceList.PriceListItems?.ToList() ?? new List<PriceListItem>());

                await _priceListUnitOfWork.PriceListRepository.RemoveAsync(priceList);
                await _priceListUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await _priceListUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PriceList?> GetPriceListByIdAsync(Guid id)
        {
            return await _priceListUnitOfWork.PriceListRepository.GetPriceListByIdAsync(id);
        }

        public async Task<(IList<PriceList> data, int total, int totalDisplay)> GetPriceListsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order)
        {
            return await _priceListUnitOfWork.PriceListRepository.GetPagedPriceListsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<decimal> GetEffectiveUnitPriceAsync(Guid productId, CustomerType customerType,
            decimal quantity, DateTime onDate)
        {
            var priceList = await _priceListUnitOfWork
                .PriceListRepository
                .GetEffectivePriceListAsync(customerType, onDate);

            var slab = priceList?.PriceListItems?
                .Where(x => x.ProductId == productId && x.MinQuantity <= quantity)
                .OrderByDescending(x => x.MinQuantity)
                .FirstOrDefault();

            if (slab != null)
            {
                return slab.UnitPrice;
            }

            // No price list covers this product, so the product's own selling price wins.
            var product = await _priceListUnitOfWork.ProductRepository.GetByIdAsync(productId)
                ?? throw new InvalidOperationException("Product not found.");

            return Convert.ToDecimal(product.SellingPrice);
        }
    }
}
