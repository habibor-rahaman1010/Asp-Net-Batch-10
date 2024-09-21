using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ProductListModel : DataTables
    {
        public ProductListModel() 
        { 
            SearchItem = new ProductSearchDto();
            Categories = new List<SelectListItem>();
            ProductTypeSelectList = new List<SelectListItem>();
        }
        public ProductSearchDto SearchItem { get; set; }
        public IList<SelectListItem> Categories { get; private set; }
        public IList<SelectListItem> ProductTypeSelectList { get; set; }

        public void SetCategoryValues(IList<Category> categories)
        {
            Categories = Utility.ConvertCategories(categories);
        }
    }
}