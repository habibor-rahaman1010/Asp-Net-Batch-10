using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class PriceListController : Controller
    {
        private readonly IPriceListManagementService _priceListManagementService;
        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<PriceListController> _logger;

        public PriceListController(IPriceListManagementService priceListManagementService,
            IProductManagementService productManagementService,
            ILogger<PriceListController> logger)
        {
            _priceListManagementService = priceListManagementService;
            _productManagementService = productManagementService;
            _logger = logger;
        }

        [Authorize(Policy = "ReadPermission")]
        public IActionResult PriceListIndex()
        {
            return View();
        }

        [HttpPost, Authorize(Policy = "ReadPermission")]
        public async Task<JsonResult> GetPriceListJsonData([FromBody] PriceListListModel model)
        {
            var result = await _priceListManagementService.GetPriceListsAsync(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression(
                    "PriceListName",
                    "CustomerType",
                    "EffectiveFrom",
                    "EffectiveTo",
                    "IsActive",
                    "Id",
                    "Id"));

            var priceListJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.PriceListName),
                            HttpUtility.HtmlEncode(record.CustomerType.ToString()),
                            HttpUtility.HtmlEncode(record.EffectiveFrom.ToString("dd-MM-yyyy")),
                            HttpUtility.HtmlEncode(record.EffectiveTo?.ToString("dd-MM-yyyy") ?? "-"),
                            HttpUtility.HtmlEncode(record.IsActive ? "Active" : "Inactive"),
                            HttpUtility.HtmlEncode((record.PriceListItems?.Count ?? 0).ToString()),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(priceListJsonData);
        }

        [Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePriceList()
        {
            var model = new PriceListCreateModel();
            var products = await _productManagementService.GetAllProductAsync();

            foreach (var item in model.PriceListItems)
            {
                item.SetProductValues(products);
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "CreatePermission")]
        public async Task<IActionResult> CreatePriceList(PriceListCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RepopulateProductsAsync(model.PriceListItems);
                return View(model);
            }

            try
            {
                var priceListId = Guid.NewGuid();

                var priceList = new PriceList
                {
                    Id = priceListId,
                    PriceListName = model.PriceListName,
                    CustomerType = model.CustomerType,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    IsActive = model.IsActive,
                    PriceListItems = BuildItems(model.PriceListItems, priceListId)
                };

                await _priceListManagementService.CreatePriceListAsync(priceList);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Price list created successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PriceListIndex));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RepopulateProductsAsync(model.PriceListItems);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Price list creation failed.");
                ModelState.AddModelError(string.Empty, "Price list creation failed.");
                await RepopulateProductsAsync(model.PriceListItems);
                return View(model);
            }
        }

        [Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePriceList(Guid id)
        {
            var priceList = await _priceListManagementService.GetPriceListByIdAsync(id);

            if (priceList == null)
            {
                return NotFound();
            }

            var model = new PriceListUpdateModel
            {
                Id = priceList.Id,
                PriceListName = priceList.PriceListName,
                CustomerType = priceList.CustomerType,
                EffectiveFrom = priceList.EffectiveFrom,
                EffectiveTo = priceList.EffectiveTo,
                IsActive = priceList.IsActive,
                PriceListItems = priceList.PriceListItems?.Select(x => new PriceListItemModel
                {
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice,
                    MinQuantity = x.MinQuantity
                }).ToList() ?? new List<PriceListItemModel>()
            };

            if (model.PriceListItems.Count == 0)
            {
                model.PriceListItems.Add(new PriceListItemModel());
            }

            await RepopulateProductsAsync(model.PriceListItems);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "UpdatePermission")]
        public async Task<IActionResult> UpdatePriceList(PriceListUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                await RepopulateProductsAsync(model.PriceListItems);
                return View(model);
            }

            try
            {
                var priceList = new PriceList
                {
                    Id = model.Id,
                    PriceListName = model.PriceListName,
                    CustomerType = model.CustomerType,
                    EffectiveFrom = model.EffectiveFrom,
                    EffectiveTo = model.EffectiveTo,
                    IsActive = model.IsActive,
                    PriceListItems = BuildItems(model.PriceListItems, model.Id)
                };

                await _priceListManagementService.UpdatePriceListAsync(priceList);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Price list updated successfully.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction(nameof(PriceListIndex));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await RepopulateProductsAsync(model.PriceListItems);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Price list update failed.");
                ModelState.AddModelError(string.Empty, "Price list update failed.");
                await RepopulateProductsAsync(model.PriceListItems);
                return View(model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Policy = "DeletePermission")]
        public async Task<JsonResult> DeletePriceList(Guid id)
        {
            try
            {
                await _priceListManagementService.DeletePriceListAsync(id);

                return Json(new
                {
                    success = true,
                    message = "Price list deleted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Price list delete failed.");

                return Json(new
                {
                    success = false,
                    message = "Price list delete failed."
                });
            }
        }

        [HttpGet, Authorize(Policy = "ReadPermission")]
        public async Task<IActionResult> GetPriceListDetails(Guid id)
        {
            var priceList = await _priceListManagementService.GetPriceListByIdAsync(id);

            if (priceList == null)
            {
                return NotFound();
            }

            var result = new
            {
                id = priceList.Id,
                priceListName = priceList.PriceListName,
                customerType = priceList.CustomerType.ToString(),
                effectiveFrom = priceList.EffectiveFrom,
                effectiveTo = priceList.EffectiveTo,
                isActive = priceList.IsActive,
                priceListItems = priceList.PriceListItems?.Select(x => new
                {
                    productName = x.Product?.ProductName,
                    minQuantity = x.MinQuantity,
                    unitPrice = x.UnitPrice
                }).ToList()
            };

            return Json(result);
        }

        private static List<PriceListItem> BuildItems(IEnumerable<PriceListItemModel> items, Guid priceListId)
        {
            return items
                .Where(x => x.ProductId != Guid.Empty && x.UnitPrice >= 0)
                .Select(x => new PriceListItem
                {
                    Id = Guid.NewGuid(),
                    PriceListId = priceListId,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice,
                    MinQuantity = x.MinQuantity <= 0 ? 1 : x.MinQuantity
                })
                .ToList();
        }

        private async Task RepopulateProductsAsync(IList<PriceListItemModel> items)
        {
            var products = await _productManagementService.GetAllProductAsync();

            foreach (var item in items)
            {
                item.SetProductValues(products);
            }
        }
    }
}
