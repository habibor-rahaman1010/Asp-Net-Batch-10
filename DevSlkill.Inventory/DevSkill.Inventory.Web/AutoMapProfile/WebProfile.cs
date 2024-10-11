using AutoMapper;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Web.Areas.Admin.Models;

namespace DevSkill.Inventory.Web.AutoMapProfile
{
    public class WebProfile : Profile 
    {
        public WebProfile()
        {
            CreateMap<ProductCreateModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();
            CreateMap<CategoryCreateModel, Category>().ReverseMap();
            CreateMap<UpdateCategoryModel, Category>().ReverseMap();

        }
    }
}
