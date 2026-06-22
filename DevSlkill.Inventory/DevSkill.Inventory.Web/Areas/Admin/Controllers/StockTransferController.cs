using DevSkill.Inventory.Application.ServicesContract;
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

        public StockTransferController(IBusinessLocationManagementService businessLocationManagementService)
        {
            _businessLocationManagementService = businessLocationManagementService;
        }

        public async Task<IActionResult> CreateStockTransfer()
        {
            var model = new StockTransferCreateModel();
            model.SetFromWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetToWarehouseValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public IActionResult CreateStockTransfer(StockTransferCreateModel model)
        {
            return View();
        }
    }
}
