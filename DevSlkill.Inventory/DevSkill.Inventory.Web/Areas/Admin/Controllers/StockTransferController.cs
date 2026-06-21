using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockTransferController : Controller
    {
        public IActionResult CreateStockTransfer()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public IActionResult CreateStockTransfer(StockTransferCreateModel model)
        {
            return View();
        }
    }
}
