using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminOnly")]
    public class StockTransferController : Controller
    {
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IUnitManagementService _unitManagementService;
        private readonly IProductManagementService _productManagementService;

        public StockTransferController(IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            IUnitManagementService unitManagementService)
        {
            _unitManagementService = unitManagementService;
            _productManagementService = productManagementService;
            _businessLocationManagementService = businessLocationManagementService;
        }

        public async Task<IActionResult> CreateStockTransfer()
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

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public IActionResult CreateStockTransfer(StockTransferCreateModel model)
        {
            return View(model);
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
