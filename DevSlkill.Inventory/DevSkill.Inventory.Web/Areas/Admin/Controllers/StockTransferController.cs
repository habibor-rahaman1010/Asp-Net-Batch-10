using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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

                    StockTransferItems = model.StockTransferItems
                    .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                    .Select(x =>
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

        public IActionResult GetStockTransferList()
        {
            return View();
        }

        
        public async Task<JsonResult> GetStockTransferJsonData([FromBody] StockTransferListModel model)
        {
            var result = await _stockTransferManagementService.GetStockTransferListAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("Id", "TransferNo", "Remarks"));

            var stockTransferJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.StockTransfer.TransferNo),
                            HttpUtility.HtmlEncode(record.Product.ProductName),
                            HttpUtility.HtmlEncode(record.StockTransfer.FromWarehouse.LocationName),
                            HttpUtility.HtmlEncode(record.StockTransfer.ToWarehouse.LocationName),
                            HttpUtility.HtmlEncode(record.Quantity),
                            HttpUtility.HtmlEncode(record.StockTransfer.TransferDate),
                            HttpUtility.HtmlEncode(record.StockTransfer.Status),
                            HttpUtility.HtmlEncode(record.StockTransfer.Remarks),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(stockTransferJsonData);
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
