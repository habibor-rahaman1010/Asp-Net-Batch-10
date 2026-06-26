using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class StockTransferManagementService : IStockTransferManagementService
    {
        private readonly IInventoryUnitOfWork _stockTransferUnitOfWork;

        public StockTransferManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _stockTransferUnitOfWork = unitOfWork;
        }

        public async Task<bool> CreateStockTransferAsync(StockTransfer model)
        {
            await _stockTransferUnitOfWork.StockTransferRepository.AddAsync(model);
            await _stockTransferUnitOfWork.SaveAsync();

            return true;
        }
    }
}
