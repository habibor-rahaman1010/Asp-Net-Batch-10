using AutoMapper;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
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

            CreateMap<CreateBrandModel, Brand>().ReverseMap();
            CreateMap<UpdateBrandModel, Brand>().ReverseMap();

            CreateMap<WarrantyCreateModel, Warranty>().ReverseMap();
            CreateMap<WarrantyUpdateModel, Warranty>().ReverseMap();

            CreateMap<UnitCreateModel, Unit>().ReverseMap();
            CreateMap<UnitUpdateModel, Unit>().ReverseMap();

            CreateMap<BusinessLocationCreateModel, BusinessLocation>().ReverseMap();
            CreateMap<BusinessLocationUpdateModel, BusinessLocation>().ReverseMap();

            CreateMap<AdjustmentTypeCreateModel, AdjustmentType>().ReverseMap();
            CreateMap<AdjustmentTypeUpdateModel, AdjustmentType>().ReverseMap();

            CreateMap<StockAdjustmentCreateModel, StockAdjustment>().ReverseMap();
            CreateMap<StockAdjustmentUpdateModel, StockAdjustment>().ReverseMap();

            CreateMap<BarcodeTypeCreateModel, BarcodeType>().ReverseMap();
            CreateMap<BarcodeTypeUpdateModel, BarcodeType>().ReverseMap();

            CreateMap<ProductTypeCreateModel, ProductType>().ReverseMap();
            CreateMap<ProductTypeUpdateModel, ProductType>().ReverseMap();
        }
    }
}
