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
        private readonly ApplicationUserManager _applicationUserManager;

        public DashboardController(IUserActivityManagementService userActivityManagementService,
            ApplicationUserManager applicationUserManager)
        {
            _userActivityManagementService = userActivityManagementService;
            _applicationUserManager = applicationUserManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalUsers = await _applicationUserManager.Users.CountAsync(),
                BounceRate = await _userActivityManagementService.CalculateBounceRate(),
                EngagementRate = 20,
                ActiveUsers = 5
            };

            return View(model);
        }
    }
}