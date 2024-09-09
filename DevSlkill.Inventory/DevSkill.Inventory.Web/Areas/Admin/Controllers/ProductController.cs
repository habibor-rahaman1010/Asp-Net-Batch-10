using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using DevSkill.Inventory.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using static DevSkill.Inventory.Web.Areas.Admin.Models.ResponseModel;
using DevSkill.Inventory.Application.Services;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductManagementService _productManagementService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProductJsonData([FromBody] ProductListModel model)
        {
            var result = _productManagementService.GetProducts(model.PageIndex, model.PageSize, model.Search, 
                model.FormatSortExpression("Id", "ProductName", "Description", "Price", "Ratings"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                                HttpUtility.HtmlEncode(record.Id),
                                HttpUtility.HtmlEncode(record.ProductName),
                                HttpUtility.HtmlEncode(record.Description),
                                HttpUtility.HtmlEncode(record.Price),
                                HttpUtility.HtmlEncode(record.Ratings),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    Ratings = model.Ratings,
                };

                if (ModelState.IsValid)
                {
                    _productManagementService.CreateProduct(product);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "Product has been created successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) 
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Product has been created successfuly",
                    Type = ResponseTypes.Success
                });
                _logger.LogError(ex, "Ultimatly product creation failed!");
            }
            return View();
        }
    }
}
