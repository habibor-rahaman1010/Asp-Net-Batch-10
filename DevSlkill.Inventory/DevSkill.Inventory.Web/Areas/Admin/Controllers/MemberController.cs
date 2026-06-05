using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevSkill.Inventory.Infrastructure;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminOnly")]
    public class MemberController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationTime _applicationTime;
        private readonly ILogger<MemberController> _logger;

        public MemberController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IApplicationTime applicationTime,
            ILogger<MemberController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _applicationTime = applicationTime;
            _logger = logger;
        }

        //This is method return lsit of role...
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ListRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        //This is method for new role create...
        [Authorize(Policy = "AdminOnly")]
        public IActionResult CreateRole()
        {
            var model = new RoleCreateModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateRole(RoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.CreateAsync(new ApplicationRole
                    {
                        Id = Guid.NewGuid(),
                        Name = model.RoleName,
                        NormalizedName = model.RoleName.ToUpper(),
                        ConcurrencyStamp = _applicationTime.GetCurrentDateTime().Ticks.ToString(),
                    });

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Role has been created successfuly!",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction(nameof(ListRoles));
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Role creation has failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "Ultimatly the Role creation failed!");
                }
            }
            return View(model);
        }

        //This is method for role Update...
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateRole(Guid id)
        {
            // Find the role by its ID
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Role not found!",
                    Type = ResponseTypes.Danger
                });
                return RedirectToAction(nameof(ListRoles));
            }

            // Map the role data to the RoleUpdateModel to pre-fill the form
            var model = new RoleUpdateModel
            {
                Id = Guid.Parse(role.Id.ToString()),
                RoleName = role.Name
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateRole(RoleUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Find the role by its ID
                    var role = await _roleManager.FindByIdAsync(model.Id.ToString());
                    if (role == null)
                    {
                        TempData.Put("ResponseMessage", new ResponseModel
                        {
                            Message = "Role not found!",
                            Type = ResponseTypes.Danger
                        });
                        return RedirectToAction(nameof(ListRoles));
                    }

                    // Update the role properties
                    role.Name = model.RoleName;
                    role.NormalizedName = model.RoleName.ToUpper();
                    role.ConcurrencyStamp = _applicationTime.GetCurrentDateTime().Ticks.ToString();

                    // Save the changes
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        TempData.Put("ResponseMessage", new ResponseModel
                        {
                            Message = "The Role has been updated successfully!",
                            Type = ResponseTypes.Success
                        });

                        return RedirectToAction(nameof(ListRoles));
                    }

                    // If update fails, add errors to ModelState
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Role update failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "An error occurred while updating the role.");
                }
            }

            return View(model);
        }

        //This is method for role delete...
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            // Find the role by its ID
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Role not found!",
                    Type = ResponseTypes.Danger
                });
                return RedirectToAction(nameof(ListRoles));
            }

            try
            {
                // Attempt to delete the role
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Role has been deleted successfully!",
                        Type = ResponseTypes.Success
                    });
                }
                else
                {
                    // If deletion fails, add errors to TempData
                    foreach (var error in result.Errors)
                    {
                        TempData.Put("ResponseMessage", new ResponseModel
                        {
                            Message = error.Description,
                            Type = ResponseTypes.Danger
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Role deletion failed!",
                    Type = ResponseTypes.Danger
                });
                _logger.LogError(ex, "An error occurred while deleting the role.");
            }

            return RedirectToAction(nameof(ListRoles));
        }

        //User Role Change method...
        [Authorize(Policy = "AdminOnly")]
        public IActionResult ChangeRole()
        {
            var model = new RoleChangeModel();
            LoadValues(model);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ChangeRole(RoleChangeModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                var newRole = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }
            LoadValues(model);
            return View(model);
        }

        private void LoadValues(RoleChangeModel model)
        {
            // Get users and roles as lists and insert the default "Select" option at the beginning
            var usersList = _userManager.Users
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Email })
                .ToList();
            usersList.Insert(0, new SelectListItem { Value = "", Text = "--Select A User--" });

            var rolesList = _roleManager.Roles
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
            rolesList.Insert(0, new SelectListItem { Value = "", Text = "--Select A Role--" });

            // Assign to model
            model.Users = new List<SelectListItem>(usersList);
            model.Roles = new List<SelectListItem>(rolesList);
        }



        //This is code for get list of claim
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ListUserClaims()
        {
            var usersWithClaims = new List<UserClaimsViewModel>();

            // Get all users
            var users = _userManager.Users.ToList();

            // Loop through each user and get their claims
            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                usersWithClaims.Add(new UserClaimsViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Claims = claims
                });
            }

            return View(usersWithClaims);
        }


        //This code for claim create
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AddClaim()
        {
            var model = new ClaimAddModel();
            model.Users = new SelectList(from c in _userManager.Users select c, "Id", "UserName");
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AddClaim(ClaimAddModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                if (user != null)
                {
                    await _userManager.AddClaimAsync(user, new Claim(model.ClaimName, model.ClaimValue));
                }
                model.Users = new SelectList(from c in _userManager.Users select c, "Id", "UserName");
                return RedirectToAction(nameof(ListUserClaims));
            }

            model.Users = new SelectList(from c in _userManager.Users select c, "Id", "UserName");
            return View(model);
        }


        //This code for delete claim of a user
       public async Task<IActionResult> DeleteClaim(Guid userId, string claimType, string claimValue)
        {
            if (userId == null || claimType == null || claimValue == null)
            {
                return BadRequest("Invalid claim deletion request.");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var claim = new Claim(claimType, claimValue);
            var result = await _userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ListUserClaims));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("ListUserClaims", await GetUsersWithClaims());
        }

        private async Task<List<UserClaimsViewModel>> GetUsersWithClaims()
        {
            var usersWithClaims = new List<UserClaimsViewModel>();
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                usersWithClaims.Add(new UserClaimsViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Claims = claims
                });
            }

            return usersWithClaims;
        }

        //This method for claim edit
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditClaim(Guid userId, string claimType, string claimValue)
        {
            if (userId == null || claimType == null || claimValue == null)
            {
                return BadRequest("Invalid claim edit request.");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Populate the model with the original claim type and value
            var model = new ClaimEditModel
            {
                UserId = userId,
                OriginalClaimType = claimType,
                OriginalClaimValue = claimValue,
                NewClaimType = claimType,
                NewClaimValue = claimValue
            };

            return View(model);
        }

        //This method for claim edit
        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditClaim(ClaimEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Remove the original claim
            var oldClaim = new System.Security.Claims.Claim(model.OriginalClaimType, model.OriginalClaimValue);
            var removeResult = await _userManager.RemoveClaimAsync(user, oldClaim);
            if (!removeResult.Succeeded)
            {
                foreach (var error in removeResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            // Add the new claim with updated type and value
            var newClaim = new System.Security.Claims.Claim(model.NewClaimType, model.NewClaimValue);
            var addResult = await _userManager.AddClaimAsync(user, newClaim);
            if (!addResult.Succeeded)
            {
                foreach (var error in addResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction(nameof(ListUserClaims));
        }

    }
}
