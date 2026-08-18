using Autofac;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using DevSkill.Inventory.Infrastructure.Data;
using DevSkill.Inventory.Infrastructure.Repositories;
using DevSkill.Inventory.Infrastructure.UnitOfWork;

using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure.MetricsServiceImplement;
using DevSkill.Inventory.Application.MetricsServiceInterface;

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
            builder.RegisterType<InventoryDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssembly", _migrationAssembly)
               .InstancePerLifetimeScope();

            builder.RegisterType<OnlineUserTrackerService>()
               .As<IOnlineUserTrackerService>()
               .SingleInstance();

            builder.RegisterType<InventoryUnitOfWork>()
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

            builder.RegisterType<StockAdjustmentRepository>()
               .As<IStockAdjustmentRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<StockAdjustmentManagementService>()
               .As<IStockAdjustmentManagementService>()
               .InstancePerLifetimeScope();

            builder.RegisterType<StockAdjustmentItemRepository>()
                .As<IStockAdjustmentItemRepository>()
                .InstancePerLifetimeScope();


            builder.RegisterType<UserActivityRepository>()
                .As<IUserActivityRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserActivityManagementService>()
              .As<IUserActivityManagementService>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockTransferItemRepository>()
               .As<IStockTransferItemRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<StockTransferRepository>()
              .As<IStockTransferRepository>()
              .InstancePerLifetimeScope();

            builder.RegisterType<StockTransferManagementService>()
                .As<IStockTransferManagementService>()
                .InstancePerLifetimeScope();

            //Sales Management module registration
            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerManagementService>()
                .As<ICustomerManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PaymentTermRepository>()
                .As<IPaymentTermRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PaymentTermManagementService>()
                .As<IPaymentTermManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceListRepository>()
                .As<IPriceListRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceListItemRepository>()
                .As<IPriceListItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceListManagementService>()
                .As<IPriceListManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DiscountRuleRepository>()
                .As<IDiscountRuleRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DiscountRuleManagementService>()
                .As<IDiscountRuleManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProformaInvoiceRepository>()
                .As<IProformaInvoiceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProformaInvoiceItemRepository>()
                .As<IProformaInvoiceItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProformaInvoiceManagementService>()
                .As<IProformaInvoiceManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DeliveryRepository>()
                .As<IDeliveryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DeliveryItemRepository>()
                .As<IDeliveryItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DeliveryManagementService>()
                .As<IDeliveryManagementService>()
                .InstancePerLifetimeScope();

            //Sales Management order-to-cash chain
            builder.RegisterType<SalespersonRepository>()
                .As<ISalespersonRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesCommissionRepository>()
                .As<ISalesCommissionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalespersonManagementService>()
                .As<ISalespersonManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesQuotationRepository>()
                .As<ISalesQuotationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesQuotationItemRepository>()
                .As<ISalesQuotationItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesQuotationManagementService>()
                .As<ISalesQuotationManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesOrderRepository>()
                .As<ISalesOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesOrderItemRepository>()
                .As<ISalesOrderItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesOrderManagementService>()
                .As<ISalesOrderManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockReservationRepository>()
                .As<IStockReservationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockReservationItemRepository>()
                .As<IStockReservationItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockReservationManagementService>()
                .As<IStockReservationManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesInvoiceRepository>()
                .As<ISalesInvoiceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesInvoiceItemRepository>()
                .As<ISalesInvoiceItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesInvoiceManagementService>()
                .As<ISalesInvoiceManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerPaymentRepository>()
                .As<ICustomerPaymentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerPaymentAllocationRepository>()
                .As<ICustomerPaymentAllocationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerPaymentManagementService>()
                .As<ICustomerPaymentManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesReturnRepository>()
                .As<ISalesReturnRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesReturnItemRepository>()
                .As<ISalesReturnItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesReturnManagementService>()
                .As<ISalesReturnManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreditNoteRepository>()
                .As<ICreditNoteRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreditNoteItemRepository>()
                .As<ICreditNoteItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CreditNoteManagementService>()
                .As<ICreditNoteManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerLedgerService>()
                .As<ICustomerLedgerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SalesReportService>()
                .As<ISalesReportService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DashboardAnalyticsService>()
                .As<IDashboardAnalyticsService>()
                .InstancePerLifetimeScope();

            //Purchase Management module
            builder.RegisterType<SupplierRepository>()
                .As<ISupplierRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierManagementService>()
                .As<ISupplierManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseRequisitionRepository>()
                .As<IPurchaseRequisitionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseRequisitionItemRepository>()
                .As<IPurchaseRequisitionItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseRequisitionManagementService>()
                .As<IPurchaseRequisitionManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseOrderRepository>()
                .As<IPurchaseOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseOrderItemRepository>()
                .As<IPurchaseOrderItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseOrderManagementService>()
                .As<IPurchaseOrderManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GoodsReceiptRepository>()
                .As<IGoodsReceiptRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GoodsReceiptItemRepository>()
                .As<IGoodsReceiptItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GoodsReceiptManagementService>()
                .As<IGoodsReceiptManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseInvoiceRepository>()
                .As<IPurchaseInvoiceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseInvoiceItemRepository>()
                .As<IPurchaseInvoiceItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseInvoiceManagementService>()
                .As<IPurchaseInvoiceManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseReturnRepository>()
                .As<IPurchaseReturnRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseReturnItemRepository>()
                .As<IPurchaseReturnItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseReturnManagementService>()
                .As<IPurchaseReturnManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierPaymentRepository>()
                .As<ISupplierPaymentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierPaymentAllocationRepository>()
                .As<ISupplierPaymentAllocationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierPaymentManagementService>()
                .As<ISupplierPaymentManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierLedgerService>()
                .As<ISupplierLedgerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseReportService>()
                .As<IPurchaseReportService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestForQuotationRepository>()
                .As<IRequestForQuotationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestForQuotationItemRepository>()
                .As<IRequestForQuotationItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestForQuotationManagementService>()
                .As<IRequestForQuotationManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierQuotationRepository>()
                .As<ISupplierQuotationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierQuotationItemRepository>()
                .As<ISupplierQuotationItemRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SupplierQuotationManagementService>()
                .As<ISupplierQuotationManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailUtility>()
                .As<IEmailUtility>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationTime>()
                .As<IApplicationTime>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MetricsService>()
                .As<IMetricsService>()
                .SingleInstance();

        }
    }
}
