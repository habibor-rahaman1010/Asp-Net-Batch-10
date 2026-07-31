using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class DashboardController : Controller
    {
        private readonly IUserActivityManagementService _userActivityManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IBrandManagementService _brandManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;

        public DashboardController(IUserActivityManagementService userActivityManagementService,
            IBrandManagementService brandManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IProductManagementService productManagementService,
            ICategoryManagementService categoryManagementService,
            ApplicationUserManager applicationUserManager)
        {
            _productManagementService = productManagementService;
            _brandManagementService = brandManagementService;
            _businessLocationManagementService = businessLocationManagementService;
            _categoryManagementService = categoryManagementService;
            _userActivityManagementService = userActivityManagementService;
            _applicationUserManager = applicationUserManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalProducts = await _productManagementService.GetTotalProductCount(),
                TotalCategoris = await _categoryManagementService.GetTotalCategoryCount(),
                TotalUsers = await _applicationUserManager.Users.CountAsync(),
                BounceRate = await _userActivityManagementService.CalculateBounceRate(),
                EngagementRate = await _userActivityManagementService.CalculateEngagementRateAsync(),
                ActiveUsers = await _userActivityManagementService.GetActiveUserCountAsync(),
                UniqueVisitors = await _userActivityManagementService.GetUniqueVisitors(),
                TotalBrands = await _brandManagementService.GetTotalBrandCount(),
                TotalWarehouse = await _businessLocationManagementService.GetTotalWarehouseCount(),
            };

            return View(model);
        }
    }
}