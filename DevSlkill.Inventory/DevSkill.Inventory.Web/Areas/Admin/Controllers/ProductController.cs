using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

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
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
