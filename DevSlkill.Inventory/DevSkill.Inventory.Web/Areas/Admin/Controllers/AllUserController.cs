using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevSkill.Inventory.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AllUserController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        ILogger<AllUserController> _logger;

        public AllUserController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            ILogger<AllUserController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        [Route("Admin/AllUser/AllUserList"), Authorize(Roles = "Admin")]
        public IActionResult AllUserList()
        {
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin")]
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
                var sortDirectionection = order[0].Direction; // Get the sort direction (asc or desc)

                // Determine which property to sort by based on the column index
                switch (columnIndex)
                {
                    case 0: // Username
                        query = sortDirectionection == "asc" ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
                        break;
                    case 1: // Email
                        query = sortDirectionection == "asc" ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                        break;

                    case 2: // Email
                        query = sortDirectionection == "asc" ? query.OrderBy(u => u.EmailConfirmed) : query.OrderByDescending(u => u.EmailConfirmed);
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
                    u.Id,
                    u.EmailConfirmed
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


        //This is delete user method...
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        // Check if the deleted user is the currently logged-in user
                        var currentUserId = _userManager.GetUserId(User);
                        if (currentUserId == user.Id.ToString())
                        {
                            await _signInManager.SignOutAsync();
                        }

                        return Json(new
                        {
                            success = true,
                            message = "The User has been deleted successfully."
                        });
                    }
                }

                return Json(new
                {
                    success = true,
                    message = "The User has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The User deleted failed");
                return Json(new
                {
                    success = true,
                    message = "The User deleted failed"
                });
            } 
        }

        //This is user update method...
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                return Json(new { success = true, data = user });
            }
            return Json(new { success = false, message = "The user not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                user = _mapper.Map(model, user);
                user.Id = model.Id;
                await _userManager.UpdateAsync(user);

                return Json(new { success = true, message = "The User updated successfully." });
            }
            return Json(new { success = false, message = "Error updating User." });
        }
    }
}
