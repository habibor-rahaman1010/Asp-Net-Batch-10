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

        public async Task<bool> CreateAsync(StockTransfer model)
        {
            var transfer = new StockTransfer
            {
                Id = Guid.NewGuid(),
                TransferNo = GenerateTransferNo(),
                TransferDate = DateTime.Now,
                FromWarehouseId = model.FromWarehouseId,
                ToWarehouseId = model.ToWarehouseId,
                Remarks = model.Remarks,
                Status = StockTransferStatus.Pending,

                StockTransferItems = model.StockTransferItems.Select(x =>
                    new StockTransferItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = x.ProductId,
                        Quantity = x.Quantity

                    }).ToList()
            };

            await _stockTransferUnitOfWork.StockTransferRepository.AddAsync(transfer);
            await _stockTransferUnitOfWork.SaveAsync();

            return true;
        }

        private string GenerateTransferNo()
        {
            return "TR" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
