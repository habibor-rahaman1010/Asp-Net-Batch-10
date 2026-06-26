using AutoMapper;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminOnly")]
    public class StockTransferController : Controller
    {
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IUnitManagementService _unitManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly IStockTransferManagementService _stockTransferManagementService;
        private readonly IMapper _mapper;

        public StockTransferController(IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            IStockTransferManagementService stockAdjustmentManagementService,
            IMapper mapper,
            IUnitManagementService unitManagementService)
        {
            _unitManagementService = unitManagementService;
            _productManagementService = productManagementService;
            _stockTransferManagementService = stockAdjustmentManagementService;
            _mapper = mapper;
            _businessLocationManagementService = businessLocationManagementService;
        }

        public async Task<IActionResult> CreateStockTransfer()
        {
            try
            {
                var model = new StockTransferCreateModel();
                model.TransferNo = GenerateTransferNo();
                model.SetFromWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
                model.SetToWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());

                foreach (var item in model.StockTransferItems)
                {
                    item.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
                    item.SetProductValues((await _productManagementService.GetAllProductAsync()).ToList());
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateStockTransfer(StockTransferCreateModel model)
        {
            try
            {
                Guid StockTransferId = Guid.NewGuid();

                var stockTransfer = new StockTransfer
                {
                    Id = StockTransferId,
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
                            StockTransferId = StockTransferId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity

                        }).ToList()
                };
                await _stockTransferManagementService.CreateStockTransferAsync(stockTransfer);
                return View(model);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        private string GenerateTransferNo()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();

            string letters = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"TR-{letters}-{DateTime.Now:dd-MM-yyyy}";
        }
    }
}
