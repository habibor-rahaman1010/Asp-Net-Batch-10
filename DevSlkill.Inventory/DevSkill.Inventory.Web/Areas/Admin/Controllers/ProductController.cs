using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using DevSkill.Inventory.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using AutoMapper;
using DevSkill.Inventory.Domain;
using static DevSkill.Inventory.Web.Areas.Admin.Models.ResponseModel;
using DevSkill.Inventory.Domain.RepositoryContracts;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductManagementService _productManagementService;
        private readonly ICategoryManagementService _categoryManagementService;
        private readonly IProductTypeManagementService _productTypeManagementService;
        private readonly IBarcodeTypeManagementService _barcodeTypeManagementService;
        private readonly IUnitManagementService _unitManagementService;
        private readonly IBrandManagementService _brandManagementService;
        private readonly ISubCategoryManagementService _subCategoryManagementService;
        private readonly IBusinessLocationManagementService _businessLocationManagementService;
        private readonly IWarrantyManagementService _warrantyManagementService;
        private readonly IApplicableTaxManagementService _applicableTaxManagementService;
        private readonly ISellingPriceTaxManagementService _sellingPriceTaxManagementService;
        private readonly IMapper _mapper;
        private readonly IApplicationTime _applicationTime;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, 
            IProductManagementService productManagementService,
            ICategoryManagementService categoryManagementService,
            IProductTypeManagementService productTypeManagementService,
            IBarcodeTypeManagementService barcodeTypeManagementService,
            IUnitManagementService unitManagementService,
            IBrandManagementService brandManagementService,
            ISubCategoryManagementService subCategoryManagementService,
            IBusinessLocationManagementService businessLocationManagementService,
            IWarrantyManagementService warrantyManagementService,
            IApplicableTaxManagementService applicableTaxManagementService,
            ISellingPriceTaxManagementService sellingPriceTaxManagementService,
            IApplicationTime applicationTime,
            IMapper mapper)

        {
            _productManagementService = productManagementService;
            _categoryManagementService = categoryManagementService;
            _productTypeManagementService = productTypeManagementService;
            _barcodeTypeManagementService = barcodeTypeManagementService;
            _unitManagementService = unitManagementService;
            _brandManagementService = brandManagementService;
            _subCategoryManagementService = subCategoryManagementService;
            _applicationTime = applicationTime;
            _businessLocationManagementService = businessLocationManagementService;
            _warrantyManagementService = warrantyManagementService;
            _applicableTaxManagementService = applicableTaxManagementService;
            _sellingPriceTaxManagementService = sellingPriceTaxManagementService;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/Product/GetProductJsonDataAsync")]
        [HttpPost]
        public async Task<JsonResult> GetProductJsonDataAsync([FromBody] ProductListModel model)
        {
            var result = await _productManagementService.GetProductsAsync(model.PageIndex, model.PageSize, model.Search, 
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
                                HttpUtility.HtmlEncode(record.SKU),
                                HttpUtility.HtmlEncode(record?.Category?.CategoryName),
                                HttpUtility.HtmlDecode(record?.Brand?.BrandName),
                                HttpUtility.HtmlEncode(record.Price),
                                HttpUtility.HtmlEncode(record.Ratings),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }

        public async Task<IActionResult> ProductList()
        {
            var model = new ProductListModel();
            model.SetCategoryValues(await _categoryManagementService.GetCategoriesAsync());
            model.SetProductTypeValues(await _productTypeManagementService.GetProductTypesAsync());
            return View(model);
        }

        //use stored procedure
        [Route("/Admin/Product/GetProductJsonDataSpAsync")]
        [HttpPost]
        public async Task<JsonResult> GetProductJsonDataSpAsync([FromBody] ProductListModel model)
        {
            var result = await _productManagementService.GetProductsSpAsync(model.PageIndex, model.PageSize, model.SearchItem,
                model.FormatSortExpression("Id", "ProductName", "Description", "Price", "Ratings"));

            var productJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {   
                                HttpUtility.HtmlEncode(record.ProductName),
                                HttpUtility.HtmlEncode(record.LocationName),
                                HttpUtility.HtmlEncode(record.Description),
                                HttpUtility.HtmlEncode((record.Price) - (record.Price / 100 * 30)),
                                HttpUtility.HtmlEncode(record.Price),
                                HttpUtility.HtmlEncode(record.SKU),
                                HttpUtility.HtmlEncode(record.CategoryName),
                                HttpUtility.HtmlEncode(record.ProductTypeName),
                                HttpUtility.HtmlEncode(record.Ratings),
                                HttpUtility.HtmlEncode(record.BrandName),
                                HttpUtility.HtmlEncode(record.ApplicableTaxName),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };

            return Json(productJsonData);
        }


        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateModel();
            model.SetCategoriesValues(await _categoryManagementService.GetCategoriesAsync());
            model.SetProductTypeValues(await _productTypeManagementService.GetProductTypesAsync());
            model.SetBarcodeTypeValues(await _barcodeTypeManagementService.GetBarCodeTypes());
            model.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
            model.SetBrandValues(await _brandManagementService.GetAllBrandAsync());
            model.SetSubcategoryValues(await _subCategoryManagementService.GetAllSubcategoryAsync());
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetWarrantyValues(await _warrantyManagementService.GetAllWarrantyAsync());
            model.SetApplicableTaxValues(await _applicableTaxManagementService.GetAllApplicablTax());
            model.SetSellingPriceTaxValues(await _sellingPriceTaxManagementService.GetAllSellingPriceTax());
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model);
                product.Category = await _categoryManagementService.GetCategoryById(model.CategoryId);
                product.Created = _applicationTime.GetCurrentDateTime();
                product.Updated = _applicationTime.GetCurrentDateTime();
                

                try
                {
                    await _productManagementService.CreateProduct(product);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The product has been created successfuly!",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The product creation has failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "Ultimatly the product creation failed!");
                }
            }
            return View();
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var model = new UpdateProductModel();
            var product = await _productManagementService.GetProductAsync(id);

            model.Id = product.Id;
            model.ProductName = product.ProductName;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Ratings = product.Ratings;

            return View(model);
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct (UpdateProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    Ratings = model.Ratings
                };

                try
                {
                    await _productManagementService.UpdateProductAsync(product);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Product has been update successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The product update has failed!",
                        Type = ResponseTypes.Danger
                    });
                    _logger.LogError(ex, "Ultimatly the product updated failed!");
                }
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
               await _productManagementService.DeleteBlogPostAsync(id);

                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The product has deleted successfuly",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "The product has deleted failed",
                    Type = ResponseTypes.Danger
                });

                _logger.LogError(ex, "The product deleted failed");
            }
            return View();
        }
    }
}
