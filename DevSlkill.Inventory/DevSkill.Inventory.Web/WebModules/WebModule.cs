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

            builder.RegisterType<ApplicationTime>()
                .As<IApplicationTime>()
                .InstancePerLifetimeScope();
        }
    }
}
