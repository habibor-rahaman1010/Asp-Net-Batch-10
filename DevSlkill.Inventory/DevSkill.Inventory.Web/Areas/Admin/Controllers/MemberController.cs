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

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class MemberController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationTime _applicationTime;
        ILogger<MemberController> _logger;

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
        public async Task<IActionResult> ListRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        //This is method for new role create...
        public IActionResult CreateRole()
        {
            var model = new RoleCreateModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
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

        [HttpPost, ValidateAntiForgeryToken]
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

    }
}
