using Autofac;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using DevSkill.Inventory.Infrastructure.Data;
using DevSkill.Inventory.Infrastructure.Repositories;
using DevSkill.Inventory.Infrastructure.UnitOfWork;
using DevSkill.Inventory.Web.Service;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Domain;

namespace DevSkill.Inventory.Web.WebModules
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();

            builder.RegisterType<InventoryDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductUnitOfWork>()
                .As<IInventoryUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductManagementService>()
                .As<IProductManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryManagementService>()
                .As<ICategoryManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductTypeRepository>()
                .As<IProductTypeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductTypeManagementService>()
                .As<IProductTypeManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BarcodeTypeRepository>()
               .As<IBarcodeTypeRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<BarcodeTypeManagementService>()
                .As<IBarcodeTypeManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitRepository>()
               .As<IUnitRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<UnitManagementService>()
                .As<IUnitManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BrandRepository>()
              .As<IBrandRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<BrandManagementService>()
                .As<IBrandManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SubCategoryRepository>()
             .As<ISubCategoryRepository>()
             .InstancePerLifetimeScope();

            builder.RegisterType<SubCategoryManagementService>()
                .As<ISubCategoryManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BusinessLocationRepository>()
             .As<IBusinessLocationRepository>()
             .InstancePerLifetimeScope();

            builder.RegisterType<BusinessLocationManagementService>()
                .As<IBusinessLocationManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WarrantyRepository>()
            .As<IWarrantyRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<WarrantyManagementService>()
                .As<IWarrantyManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicableTaxRepository>()
            .As<IApplicableTaxRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<ApplicableTaxManagementService>()
                .As<IApplicableTaxManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SellingPriceTaxRepository>()
            .As<ISellingPriceTaxRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<SellingPriceTaxManagementService>()
                .As<ISellingPriceTaxManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AdjustmentTypeRepository>()
           .As<IAdjustmentTypeRepository>()
           .InstancePerLifetimeScope();

            builder.RegisterType<AdjustmentTypeManagementService>()
                .As<IAdjustmentTypeManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationTime>()
                .As<IApplicationTime>()
                .InstancePerLifetimeScope();

        }
    }
}
