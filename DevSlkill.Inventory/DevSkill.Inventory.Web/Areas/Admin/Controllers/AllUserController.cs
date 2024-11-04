using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AllUserController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ILogger<AllUserController> _logger;

        public AllUserController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AllUserController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        [Route("Admin/AllUser/AllUserList")]
        public IActionResult AllUserList()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/AllUser/GetAllUsers")]
        public IActionResult GetAllUsers(int draw, int start, int length, string search, List<Order> order)
        {
            var query = _userManager.Users.AsQueryable();

            // Filter by search term if it is provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.UserName.Contains(search) || u.Email.Contains(search));
            }

            // Total count of records after filtering
            var filteredCount = query.Count();

            // Handle sorting
            if (order != null && order.Count > 0)
            {
                var columnIndex = order[0].Column; // Get the column index for sorting
                var sortDirection = order[0].Dir; // Get the sort direction (asc or desc)

                // Determine which property to sort by based on the column index
                switch (columnIndex)
                {
                    case 0: // Username
                        query = sortDirection == "asc" ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
                        break;
                    case 1: // Email
                        query = sortDirection == "asc" ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                        break;
                    // Add more cases for additional columns if necessary
                    default:
                        break;
                }
            }

            // Get paginated and filtered user data
            var users = query
                .Skip(start)
                .Take(length)
                .Select(u => new
                {
                    u.UserName,
                    u.Email,
                    u.Id
                })
                .ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = _userManager.Users.Count(),  // Total records without filtering
                recordsFiltered = filteredCount,             // Total records after filtering
                data = users
            });
        }

        // Define the Order class to match the incoming sorting parameters
        public class Order
        {
            public int Column { get; set; }
            public string Dir { get; set; }
        }
    }
}
