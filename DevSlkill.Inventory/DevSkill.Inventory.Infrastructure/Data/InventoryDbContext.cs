using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.NotificationEntities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using DevSkill.Inventory.Domain.Entities.StockTransferEntities;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public InventoryDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Every table's mapping lives in its own IEntityTypeConfiguration under
            // Configurations. Picking them up by assembly rather than naming each one
            // means a new entity is mapped by adding its configuration file and
            // nothing else: there is no list here to forget to add it to.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        //Here is my all applcation DbSet<T>
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<BarcodeType> BarcodeTypes => Set<BarcodeType>();
        public DbSet<BusinessLocation> BusinessLocations => Set<BusinessLocation>();
        public DbSet<Warranty> Warranties => Set<Warranty>();
        public DbSet<SellingPriceTax> SellingPriceTaxes => Set<SellingPriceTax>();
        public DbSet<ProductType> ProductTypes => Set<ProductType>();
        public DbSet<ApplicableTax> ApplicableTaxs => Set<ApplicableTax>();
        public DbSet<AdjustmentType> AdjustmentTypes => Set<AdjustmentType>();
        public DbSet<StockAdjustment> StockAdjustments => Set<StockAdjustment>();
        public DbSet<StockAdjustmentItem> StockAdjustmentItems => Set<StockAdjustmentItem>();
        public DbSet<ApplicationLog> ApplicationLogs => Set<ApplicationLog>();
        public DbSet<UserActivity> UserActivities => Set<UserActivity>();
        public DbSet<UserActivityLog> UserActivityLogs => Set<UserActivityLog>();
        public DbSet<StockTransfer> StockTransfers => Set<StockTransfer>();
        public DbSet<StockTransferItem> StockTransferItems => Set<StockTransferItem>();

        //Sales Management module DbSet<T>
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<PaymentTerm> PaymentTerms => Set<PaymentTerm>();
        public DbSet<PriceList> PriceLists => Set<PriceList>();
        public DbSet<PriceListItem> PriceListItems => Set<PriceListItem>();
        public DbSet<DiscountRule> DiscountRules => Set<DiscountRule>();
        public DbSet<ProformaInvoice> ProformaInvoices => Set<ProformaInvoice>();
        public DbSet<ProformaInvoiceItem> ProformaInvoiceItems => Set<ProformaInvoiceItem>();
        public DbSet<Delivery> Deliveries => Set<Delivery>();
        public DbSet<DeliveryItem> DeliveryItems => Set<DeliveryItem>();
        public DbSet<Salesperson> Salespersons => Set<Salesperson>();
        public DbSet<SalesQuotation> SalesQuotations => Set<SalesQuotation>();
        public DbSet<SalesQuotationItem> SalesQuotationItems => Set<SalesQuotationItem>();
        public DbSet<SalesOrder> SalesOrders => Set<SalesOrder>();
        public DbSet<SalesOrderItem> SalesOrderItems => Set<SalesOrderItem>();
        public DbSet<StockReservation> StockReservations => Set<StockReservation>();
        public DbSet<StockReservationItem> StockReservationItems => Set<StockReservationItem>();
        public DbSet<SalesInvoice> SalesInvoices => Set<SalesInvoice>();
        public DbSet<SalesInvoiceItem> SalesInvoiceItems => Set<SalesInvoiceItem>();
        public DbSet<CustomerPayment> CustomerPayments => Set<CustomerPayment>();
        public DbSet<CustomerPaymentAllocation> CustomerPaymentAllocations => Set<CustomerPaymentAllocation>();
        public DbSet<SalesReturn> SalesReturns => Set<SalesReturn>();
        public DbSet<SalesReturnItem> SalesReturnItems => Set<SalesReturnItem>();
        public DbSet<CreditNote> CreditNotes => Set<CreditNote>();
        public DbSet<CreditNoteItem> CreditNoteItems => Set<CreditNoteItem>();
        public DbSet<SalesCommission> SalesCommissions => Set<SalesCommission>();

        //Purchase Management module DbSet<T>
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<PurchaseRequisition> PurchaseRequisitions => Set<PurchaseRequisition>();
        public DbSet<PurchaseRequisitionItem> PurchaseRequisitionItems => Set<PurchaseRequisitionItem>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
        public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
        public DbSet<GoodsReceiptItem> GoodsReceiptItems => Set<GoodsReceiptItem>();
        public DbSet<PurchaseInvoice> PurchaseInvoices => Set<PurchaseInvoice>();
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems => Set<PurchaseInvoiceItem>();
        public DbSet<PurchaseReturn> PurchaseReturns => Set<PurchaseReturn>();
        public DbSet<PurchaseReturnItem> PurchaseReturnItems => Set<PurchaseReturnItem>();
        public DbSet<SupplierPayment> SupplierPayments => Set<SupplierPayment>();
        public DbSet<SupplierPaymentAllocation> SupplierPaymentAllocations => Set<SupplierPaymentAllocation>();
        public DbSet<RequestForQuotation> RequestForQuotations => Set<RequestForQuotation>();
        public DbSet<RequestForQuotationItem> RequestForQuotationItems => Set<RequestForQuotationItem>();
        public DbSet<SupplierQuotation> SupplierQuotations => Set<SupplierQuotation>();
        public DbSet<SupplierQuotationItem> SupplierQuotationItems => Set<SupplierQuotationItem>();

        //Notification module DbSet<T>
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<NotificationRecipient> NotificationRecipients => Set<NotificationRecipient>();
        public DbSet<NotificationSubscription> NotificationSubscriptions => Set<NotificationSubscription>();
    }
}
