using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using DevSkill.Inventory.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using static DevSkill.Inventory.Web.Areas.Admin.Models.ResponseModel;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductManagementService _productManagementService;

        public ProductController(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }

        public IActionResult Index()
        {
            return View();
        }

   
        public JsonResult GetProductJsonData(ProductListModel model)
        {
            var result = _productManagementService.GetProducts(model.PageIndex, model.PageSize, model.Search, model.FormatSortExpression("ProductName"));

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
            var product = new Product { 
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
            return View();
        }
    }
}
