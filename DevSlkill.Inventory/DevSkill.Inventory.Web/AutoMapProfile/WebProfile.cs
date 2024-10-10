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

            /*// If you need two-way mapping, include this:
            CreateMap<Product, UpdateProductModel>();

            CreateMap<UpdateProductModel, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.BarcodeType, opt => opt.Ignore())
            .ForMember(dest => dest.Unit, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.Subcategory, opt => opt.Ignore())
            .ForMember(dest => dest.BusinessLocation, opt => opt.Ignore())
            .ForMember(dest => dest.Warranty, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicableTax, opt => opt.Ignore())
            .ForMember(dest => dest.SellingPriceTax, opt => opt.Ignore())
            .ForMember(dest => dest.ProductType, opt => opt.Ignore());*/

        }
    }
}
