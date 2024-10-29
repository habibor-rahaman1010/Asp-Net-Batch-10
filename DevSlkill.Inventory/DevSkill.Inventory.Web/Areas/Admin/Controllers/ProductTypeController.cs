using AutoMapper;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeManagementService _productTypeManagementService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductTypeController(IProductTypeManagementService productTypeManagementService,
            IMapper mapper,
            ILogger<ProductTypeController> logger)
        {
            _productTypeManagementService = productTypeManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult ProductTypeList()
        {
            return View();
        }
        //Get all product type data 
        public async Task<JsonResult> GetProductTypeJsonData([FromBody] ProductTypeListModel model)
        {
            var result = await _productTypeManagementService.GetAllProductTypeAsync(model.PageIndex, model.PageSize, model.Search,
               model.FormatSortExpression("Id, ProductTypeName", "Description", "ProductTypeCode"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(record.ProductTypeName),
                            HttpUtility.HtmlEncode(record.Description),
                            HttpUtility.HtmlEncode(record.ProductTypeCode),
                            HttpUtility.HtmlEncode(record.Id.ToString())
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        //This is method for create new product type
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> AddProductType(ProductTypeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var productType = _mapper.Map<ProductType>(model);
                productType.Id = Guid.NewGuid();

                try
                {
                    await _productTypeManagementService.AddProductTypeAsync(productType);

                    return Json(new
                    {
                        success = true,
                        message = "Product type created successfully"
                    });
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Product creation failed");
                    return Json(new
                    {
                        success = false,
                        message = "Product type creation failed"
                    });
                }
            }
            return Json(new
            {
                success = false,
                message = "Product type creation failed"
            });
        }

        //This code for product type update
        public async Task<IActionResult> GetProductTypeById(Guid id)
        {
            var productType = await _productTypeManagementService.GetProductTypeIdAsync(id);
            if (productType != null)
            {
                return Json(new { success = true, data = productType });
            }
            return Json(new { success = false, message = "Product Type not found." });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProductType(ProductTypeUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                var productType = await _productTypeManagementService.GetProductTypeIdAsync(model.Id);
                productType = _mapper.Map(model, productType);
                productType.Id = model.Id;
                await _productTypeManagementService.UpdateProductTypeAsync(productType);

                return Json(new { success = true, message = "Product Type updated successfully." });
            }
            return Json(new { success = false, message = "Error updating Product Type." });
        }

        //This code for delete
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteProductType(Guid id)
        {
            try
            {
                await _productTypeManagementService.DeleteProductTypeAsync(id);

                return Json(new
                {
                    success = true,
                    message = "The Product Type has deleted successfuly"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Product Type deleted failed");
                return Json(new
                {
                    success = false,
                    message = "The Product Type deleted failed"
                });
            }
        }
    }
}
