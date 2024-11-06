using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevSkill.Inventory.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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

        //This method return all users with pagination and with column sorting...
        [Route("Admin/AllUser/AllUserList"), Authorize(Roles = "Admin, Member")]
        public IActionResult AllUserList()
        {
            return View();
        }

        //This method return all users with pagination and with column sorting...
        [HttpPost, Authorize(Roles = "Admin, Member")]
        [Route("Admin/AllUser/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int draw, int start, int length, string search, List<Order> order)
        {
            var query = _userManager.Users.AsQueryable();

            // Filter by search term if it is provided
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.UserName.Contains(search) || u.Email.Contains(search));
            }

            // Total count of records after filtering
            var filteredCount = query.Count();

            // Get paginated user data
            var usersList = query
                .Skip(start)
                .Take(length)
                .ToList();

            // Create a list to store user details along with roles
            var usersWithRoles = new List<(ApplicationUser User, string RoleNames)>();

            // Fetch roles for each user
            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleNames = string.Join(", ", roles);
                usersWithRoles.Add((user, roleNames));
            }

            // Handle sorting
            if (order != null && order.Count > 0)
            {
                var columnIndex = order[0].Column; // Get the column index for sorting
                var sortDirection = order[0].Direction; // Get the sort direction (asc or desc)

                // Sort based on the selected column
                switch (columnIndex)
                {
                    case 0: // Username
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.UserName).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.UserName).ToList();
                        break;

                    case 1: // Email
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.Email).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.Email).ToList();
                        break;

                    case 2: // EmailConfirmed
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.User.EmailConfirmed).ToList()
                            : usersWithRoles.OrderByDescending(u => u.User.EmailConfirmed).ToList();
                        break;

                    case 3: // RoleNames
                        usersWithRoles = sortDirection == "asc"
                            ? usersWithRoles.OrderBy(u => u.RoleNames).ToList()
                            : usersWithRoles.OrderByDescending(u => u.RoleNames).ToList();
                        break;

                    default:
                        break;
                }
            }

            // Prepare the final data to return
            var data = usersWithRoles.Select(u => new
            {
                u.User.UserName,
                u.User.Email,
                u.User.Id,
                u.User.EmailConfirmed,
                RoleNames = u.RoleNames
            }).ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = _userManager.Users.Count(),  // Total records without filtering
                recordsFiltered = filteredCount,             // Total records after filtering
                data = data
            });
        }


        //This is delete user method...
        [HttpPost, Authorize(Roles = "Admin")]
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


        //This mehtod get a user by id for update...
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return Json(new { success = false });
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            return Json(new
            {
                success = true,
                data = new
                {
                    id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    address = user.Address,
                    userRoles = userRoles,       // Current roles
                    availableRoles = allRoles    // All available roles
                }
            });
        }

        //This is user update mehtod...
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model, List<string> Roles)
        {
            if (ModelState.IsValid)
            {
                // Get the user
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user == null)
                {
                    return Json(new { success = false, message = "User not found." });
                }

                // Update user properties
                user = _mapper.Map(model, user);

                var result = await _userManager.UpdateAsync(user);

                // Update user roles
                var currentRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return Json(new { success = false, message = "Failed to remove user roles." });
                }

                var addResult = await _userManager.AddToRolesAsync(user, Roles);
                if (!addResult.Succeeded)
                {
                    return Json(new { success = false, message = "Failed to add new roles." });
                }

                return Json(new { success = true, message = "User updated successfully." });
            }
            return Json(new { success = false, message = "Invalid data." });
        }

    }
}
