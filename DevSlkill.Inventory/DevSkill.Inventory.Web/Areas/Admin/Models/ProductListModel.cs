using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductListModel : DataTables
    {
        public ProductSearchDto SearchItem { get; set; }
        public IList<SelectListItem> Categories { get; private set; }
        public IList<SelectListItem> ProductTypes { get; private set; }

        public ProductListModel() 
        { 
            SearchItem = new ProductSearchDto();
            Categories = new List<SelectListItem>();
            ProductTypes = new List<SelectListItem>();
        }

        public void SetCategoryValues(IList<Category> categories)
        {
            Categories = Utility.ConvertCategories(categories);
        }

        public void SetProductTypeValues(IList<ProductType> productTypes)
        {
            ProductTypes = Utility.ConvertProductTypes(productTypes);
        }
    }
}