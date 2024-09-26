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
                                HttpUtility.HtmlDecode(record.Description),
                                HttpUtility.HtmlEncode(record.SKU),
                                HttpUtility.HtmlEncode(record?.Category?.CategoryName),
                                HttpUtility.HtmlEncode(record?.Brand?.BrandName),
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
                                HttpUtility.HtmlDecode(record.Description),
                                HttpUtility.HtmlEncode((record.Price - (record.Price / 100 * 30)).ToString("F2")),
                                HttpUtility.HtmlEncode(record.Price.ToString("F2")),
                                HttpUtility.HtmlEncode(record.SellingPrice.ToString("F2")),
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
                product.ProductType = await _productTypeManagementService.GetProductTypeIdAsync(model.ProductypeId);
                product.BarcodeType = await _barcodeTypeManagementService.GetBarcodeTypeId(model.BarcodeTypeId);
                product.Unit = await _unitManagementService.GetUnitByIdAsync(model.UnitId);
                product.Brand = await _brandManagementService.GetBrandByIdAsync(model.BrandId);
                product.Subcategory = await _subCategoryManagementService.GetSubcategoryByIdAsync(model.SubcategoryId);
                product.BusinessLocation = await _businessLocationManagementService.GetBusinessLocationByIdAsync(model.BusinessLocationId);
                product.Warranty = await _warrantyManagementService.GetWarrantyByIdAsync(model.WarrantyId);
                product.ApplicableTax = await _applicableTaxManagementService.GetApplicableTaxByIdAsync(model.ApplicableTaxId);
                product.SellingPriceTax = await _sellingPriceTaxManagementService.GetSellingPriceTaxByIdAsync(model.SellingPriceTaxId);
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

                    return RedirectToAction("ProductList");
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
            /*model.SetCategoriesValues(await _categoryManagementService.GetCategoriesAsync());
            model.SetProductTypeValues(await _productTypeManagementService.GetProductTypesAsync());
            model.SetBarcodeTypeValues(await _barcodeTypeManagementService.GetBarCodeTypes());
            model.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
            model.SetBrandValues(await _brandManagementService.GetAllBrandAsync());
            model.SetSubcategoryValues(await _subCategoryManagementService.GetAllSubcategoryAsync());
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetWarrantyValues(await _warrantyManagementService.GetAllWarrantyAsync());
            model.SetApplicableTaxValues(await _applicableTaxManagementService.GetAllApplicablTax());
            model.SetSellingPriceTaxValues(await _sellingPriceTaxManagementService.GetAllSellingPriceTax());*/
            return View(model);
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var product = await _productManagementService.GetProductAsync(id);
            var model = _mapper.Map<UpdateProductModel>(product);

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

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct (UpdateProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _productManagementService.GetProductAsync(model.Id);
                product = _mapper.Map(model, product);

                product.Category = await _categoryManagementService.GetCategoryById(model.CategoryId);
                product.BarcodeType = await _barcodeTypeManagementService.GetBarcodeTypeId(model.BarcodeTypeId);
                product.Unit = await _unitManagementService.GetUnitByIdAsync(model.UnitId);
                product.Brand = await _brandManagementService.GetBrandByIdAsync(model.BrandId);
                product.Subcategory = await _subCategoryManagementService.GetSubcategoryByIdAsync(model.SubcategoryId);
                product.BusinessLocation = await _businessLocationManagementService.GetBusinessLocationByIdAsync(model.BusinessLocationId);
                product.Warranty = await _warrantyManagementService.GetWarrantyByIdAsync(model.WarrantyId);
                product.ApplicableTax = await _applicableTaxManagementService.GetApplicableTaxByIdAsync(model.ApplicableTaxId);
                product.SellingPriceTax = await _sellingPriceTaxManagementService.GetSellingPriceTaxByIdAsync(model.SellingPriceTaxId);
                product.ProductType = await _productTypeManagementService.GetProductTypeIdAsync(model.ProductTypeId);
                product.Updated = _applicationTime.GetCurrentDateTime();

                try
                {
                    await _productManagementService.UpdateProductAsync(product);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "The Product has been update successfuly",
                        Type = ResponseTypes.Success
                    });

                    return RedirectToAction("ProductList");
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

            model.SetCategoriesValues(await _categoryManagementService.GetCategoriesAsync());
            model.SetBarcodeTypeValues(await _barcodeTypeManagementService.GetBarCodeTypes());
            model.SetUnitValues(await _unitManagementService.GetAllUnitAsync());
            model.SetBrandValues(await _brandManagementService.GetAllBrandAsync());
            model.SetSubcategoryValues(await _subCategoryManagementService.GetAllSubcategoryAsync());
            model.SetBusinessLocationValues(await _businessLocationManagementService.GetAllBusinessLocationAsync());
            model.SetWarrantyValues(await _warrantyManagementService.GetAllWarrantyAsync());
            model.SetApplicableTaxValues(await _applicableTaxManagementService.GetAllApplicablTax());
            model.SetSellingPriceTaxValues(await _sellingPriceTaxManagementService.GetAllSellingPriceTax());
            model.SetProductTypeValues(await _productTypeManagementService.GetProductTypesAsync());

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
               await _productManagementService.DeleteProductAsync(id);

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
