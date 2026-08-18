using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using DevSkill.Inventory.Infrastructure.Data;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Infrastructure.UnitOfWork
{
    public class InventoryUnitOfWork : UnitOfWork, IInventoryUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductTypeRepository ProductTypeRepository { get; private set; }
        public IBarcodeTypeRepository BarcodeTypeRepository {  get; private set; }
        public IUnitRepository UnitRepository { get; private set; }
        public IBrandRepository BrandRepository { get; private set; }
        public ISubCategoryRepository SubCategoryRepository { get; private set; }
        public IBusinessLocationRepository BusinessLocationRepository { get; private set; }
        public IWarrantyRepository WarrantyRepository { get; private set; }
        public IApplicableTaxRepository ApplicableTaxRepository { get; private set; }
        public ISellingPriceTaxRepository SellingPriceTaxRepository { get; private set; }
        public IAdjustmentTypeRepository AdjustmentTypeRepository { get; private set; }
        public IStockAdjustmentRepository StockAdjustmentRepository { get; private set; }
        public IStockAdjustmentItemRepository StockAdjustmentItemRepository { get; private set; }
        public IUserActivityRepository UserActivityRepository { get; private set; }

        public IStockTransferRepository StockTransferRepository { get; private set; }

        public IStockTransferItemRepository StockTransferItemRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public IPaymentTermRepository PaymentTermRepository { get; private set; }

        public IPriceListRepository PriceListRepository { get; private set; }

        public IPriceListItemRepository PriceListItemRepository { get; private set; }

        public IDiscountRuleRepository DiscountRuleRepository { get; private set; }

        public IProformaInvoiceRepository ProformaInvoiceRepository { get; private set; }

        public IProformaInvoiceItemRepository ProformaInvoiceItemRepository { get; private set; }

        public IDeliveryRepository DeliveryRepository { get; private set; }

        public IDeliveryItemRepository DeliveryItemRepository { get; private set; }

        public ISalespersonRepository SalespersonRepository { get; private set; }

        public ISalesQuotationRepository SalesQuotationRepository { get; private set; }

        public ISalesQuotationItemRepository SalesQuotationItemRepository { get; private set; }

        public ISalesOrderRepository SalesOrderRepository { get; private set; }

        public ISalesOrderItemRepository SalesOrderItemRepository { get; private set; }

        public IStockReservationRepository StockReservationRepository { get; private set; }

        public IStockReservationItemRepository StockReservationItemRepository { get; private set; }

        public ISalesInvoiceRepository SalesInvoiceRepository { get; private set; }

        public ISalesInvoiceItemRepository SalesInvoiceItemRepository { get; private set; }

        public ICustomerPaymentRepository CustomerPaymentRepository { get; private set; }

        public ICustomerPaymentAllocationRepository CustomerPaymentAllocationRepository { get; private set; }

        public ISalesReturnRepository SalesReturnRepository { get; private set; }

        public ISalesReturnItemRepository SalesReturnItemRepository { get; private set; }

        public ICreditNoteRepository CreditNoteRepository { get; private set; }

        public ICreditNoteItemRepository CreditNoteItemRepository { get; private set; }

        public ISalesCommissionRepository SalesCommissionRepository { get; private set; }

        public ISupplierRepository SupplierRepository { get; private set; }

        public IPurchaseRequisitionRepository PurchaseRequisitionRepository { get; private set; }

        public IPurchaseRequisitionItemRepository PurchaseRequisitionItemRepository { get; private set; }

        public IPurchaseOrderRepository PurchaseOrderRepository { get; private set; }

        public IPurchaseOrderItemRepository PurchaseOrderItemRepository { get; private set; }

        public IGoodsReceiptRepository GoodsReceiptRepository { get; private set; }

        public IGoodsReceiptItemRepository GoodsReceiptItemRepository { get; private set; }

        public IPurchaseInvoiceRepository PurchaseInvoiceRepository { get; private set; }

        public IPurchaseInvoiceItemRepository PurchaseInvoiceItemRepository { get; private set; }

        public IPurchaseReturnRepository PurchaseReturnRepository { get; private set; }

        public IPurchaseReturnItemRepository PurchaseReturnItemRepository { get; private set; }

        public ISupplierPaymentRepository SupplierPaymentRepository { get; private set; }

        public ISupplierPaymentAllocationRepository SupplierPaymentAllocationRepository { get; private set; }

        public IRequestForQuotationRepository RequestForQuotationRepository { get; private set; }

        public IRequestForQuotationItemRepository RequestForQuotationItemRepository { get; private set; }

        public ISupplierQuotationRepository SupplierQuotationRepository { get; private set; }

        public ISupplierQuotationItemRepository SupplierQuotationItemRepository { get; private set; }

        public INotificationRepository NotificationRepository { get; private set; }

        public INotificationRecipientRepository NotificationRecipientRepository { get; private set; }

        public INotificationSubscriptionRepository NotificationSubscriptionRepository { get; private set; }

        public InventoryUnitOfWork(InventoryDbContext productDbContext,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductTypeRepository productTypeRepository,
            IBarcodeTypeRepository barcodeTypeRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository,
            ISubCategoryRepository subCategoryRepository, 
            IWarrantyRepository warrantyRepository,
            IBusinessLocationRepository businessLocationRepository, 
            IApplicableTaxRepository applicableTaxRepository,
            ISellingPriceTaxRepository sellingPriceTaxRepository,
            IAdjustmentTypeRepository adjustmentTypeRepository,
            IStockAdjustmentRepository stockAdjustmentRepository,
            IStockAdjustmentItemRepository stockAdjustmentItemRepository,
            IStockTransferRepository stockTransferRepository,
            IStockTransferItemRepository stockTransferItemRepository,
            IUserActivityRepository userActivityRepository,
            ICustomerRepository customerRepository,
            IPaymentTermRepository paymentTermRepository,
            IPriceListRepository priceListRepository,
            IPriceListItemRepository priceListItemRepository,
            IDiscountRuleRepository discountRuleRepository,
            IProformaInvoiceRepository proformaInvoiceRepository,
            IProformaInvoiceItemRepository proformaInvoiceItemRepository,
            IDeliveryRepository deliveryRepository,
            IDeliveryItemRepository deliveryItemRepository,
            ISalespersonRepository salespersonRepository,
            ISalesQuotationRepository salesQuotationRepository,
            ISalesQuotationItemRepository salesQuotationItemRepository,
            ISalesOrderRepository salesOrderRepository,
            ISalesOrderItemRepository salesOrderItemRepository,
            IStockReservationRepository stockReservationRepository,
            IStockReservationItemRepository stockReservationItemRepository,
            ISalesInvoiceRepository salesInvoiceRepository,
            ISalesInvoiceItemRepository salesInvoiceItemRepository,
            ICustomerPaymentRepository customerPaymentRepository,
            ICustomerPaymentAllocationRepository customerPaymentAllocationRepository,
            ISalesReturnRepository salesReturnRepository,
            ISalesReturnItemRepository salesReturnItemRepository,
            ICreditNoteRepository creditNoteRepository,
            ICreditNoteItemRepository creditNoteItemRepository,
            ISalesCommissionRepository salesCommissionRepository,
            ISupplierRepository supplierRepository,
            IPurchaseRequisitionRepository purchaseRequisitionRepository,
            IPurchaseRequisitionItemRepository purchaseRequisitionItemRepository,
            IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderItemRepository purchaseOrderItemRepository,
            IGoodsReceiptRepository goodsReceiptRepository,
            IGoodsReceiptItemRepository goodsReceiptItemRepository,
            IPurchaseInvoiceRepository purchaseInvoiceRepository,
            IPurchaseInvoiceItemRepository purchaseInvoiceItemRepository,
            IPurchaseReturnRepository purchaseReturnRepository,
            IPurchaseReturnItemRepository purchaseReturnItemRepository,
            ISupplierPaymentRepository supplierPaymentRepository,
            ISupplierPaymentAllocationRepository supplierPaymentAllocationRepository,
            IRequestForQuotationRepository requestForQuotationRepository,
            IRequestForQuotationItemRepository requestForQuotationItemRepository,
            ISupplierQuotationRepository supplierQuotationRepository,
            ISupplierQuotationItemRepository supplierQuotationItemRepository,
            INotificationRepository notificationRepository,
            INotificationRecipientRepository notificationRecipientRepository,
            INotificationSubscriptionRepository notificationSubscriptionRepository)
            : base(productDbContext)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            ProductTypeRepository = productTypeRepository;
            BarcodeTypeRepository = barcodeTypeRepository;
            UnitRepository = unitRepository;
            BrandRepository = brandRepository;
            SubCategoryRepository = subCategoryRepository;
            BusinessLocationRepository = businessLocationRepository;
            WarrantyRepository = warrantyRepository;
            ApplicableTaxRepository = applicableTaxRepository;
            SellingPriceTaxRepository = sellingPriceTaxRepository;
            AdjustmentTypeRepository = adjustmentTypeRepository;
            StockAdjustmentRepository = stockAdjustmentRepository;
            StockAdjustmentItemRepository = stockAdjustmentItemRepository;
            UserActivityRepository = userActivityRepository;
            StockTransferRepository = stockTransferRepository;
            StockTransferItemRepository = stockTransferItemRepository;
            CustomerRepository = customerRepository;
            PaymentTermRepository = paymentTermRepository;
            PriceListRepository = priceListRepository;
            PriceListItemRepository = priceListItemRepository;
            DiscountRuleRepository = discountRuleRepository;
            ProformaInvoiceRepository = proformaInvoiceRepository;
            ProformaInvoiceItemRepository = proformaInvoiceItemRepository;
            DeliveryRepository = deliveryRepository;
            DeliveryItemRepository = deliveryItemRepository;
            SalespersonRepository = salespersonRepository;
            SalesQuotationRepository = salesQuotationRepository;
            SalesQuotationItemRepository = salesQuotationItemRepository;
            SalesOrderRepository = salesOrderRepository;
            SalesOrderItemRepository = salesOrderItemRepository;
            StockReservationRepository = stockReservationRepository;
            StockReservationItemRepository = stockReservationItemRepository;
            SalesInvoiceRepository = salesInvoiceRepository;
            SalesInvoiceItemRepository = salesInvoiceItemRepository;
            CustomerPaymentRepository = customerPaymentRepository;
            CustomerPaymentAllocationRepository = customerPaymentAllocationRepository;
            SalesReturnRepository = salesReturnRepository;
            SalesReturnItemRepository = salesReturnItemRepository;
            CreditNoteRepository = creditNoteRepository;
            CreditNoteItemRepository = creditNoteItemRepository;
            SalesCommissionRepository = salesCommissionRepository;
            SupplierRepository = supplierRepository;
            PurchaseRequisitionRepository = purchaseRequisitionRepository;
            PurchaseRequisitionItemRepository = purchaseRequisitionItemRepository;
            PurchaseOrderRepository = purchaseOrderRepository;
            PurchaseOrderItemRepository = purchaseOrderItemRepository;
            GoodsReceiptRepository = goodsReceiptRepository;
            GoodsReceiptItemRepository = goodsReceiptItemRepository;
            PurchaseInvoiceRepository = purchaseInvoiceRepository;
            PurchaseInvoiceItemRepository = purchaseInvoiceItemRepository;
            PurchaseReturnRepository = purchaseReturnRepository;
            PurchaseReturnItemRepository = purchaseReturnItemRepository;
            SupplierPaymentRepository = supplierPaymentRepository;
            SupplierPaymentAllocationRepository = supplierPaymentAllocationRepository;
            RequestForQuotationRepository = requestForQuotationRepository;
            RequestForQuotationItemRepository = requestForQuotationItemRepository;
            SupplierQuotationRepository = supplierQuotationRepository;
            SupplierQuotationItemRepository = supplierQuotationItemRepository;
            NotificationRepository = notificationRepository;
            NotificationRecipientRepository = notificationRecipientRepository;
            NotificationSubscriptionRepository = notificationSubscriptionRepository;
        }

        public async Task<(IList<ProductDto> data, int total, int totalDisplay)> GetPagedProductUsingSPAsync(int pageIndex, int pageSize, ProductSearchDto search, string? order)
        {
            var procedureName = "ProductAdvancedSearch";
            var result = await SqlUtility.QueryWithStoredProcedureAsync<ProductDto>(procedureName, new Dictionary<string, object>
            {
                { "PageIndex", pageIndex },
                { "PageSize", pageSize },
                { "OrderBy", order },
                { "ProductName", string.IsNullOrEmpty(search.ProductName) ? null : search.ProductName },
                { "ProductTypeId", string.IsNullOrEmpty(search.ProductTypeId) ? null : Guid.Parse(search.ProductTypeId) },
                { "CategoryId", string.IsNullOrEmpty(search.CategoryId) ? null : Guid.Parse(search.CategoryId) },
                { "BrandId", string.IsNullOrEmpty(search.BrandId) ? null : Guid.Parse(search.BrandId)},
                { "UnitId", string.IsNullOrEmpty(search.UnitId) ? null : Guid.Parse(search.UnitId)},
                { "BusinessLocationId", string.IsNullOrEmpty(search.BusinessLocationId) ? null : Guid.Parse(search.BusinessLocationId)},
                { "SellingPriceTaxId", string.IsNullOrEmpty(search.SellingPriceTaxId) ? null : Guid.Parse(search.SellingPriceTaxId)},
                { "ApplicableTaxId", string.IsNullOrEmpty(search.ApplicableTaxId) ? null : Guid.Parse(search.ApplicableTaxId)},
            },

            new Dictionary<string, Type>
            {
                { "Total", typeof(int) },
                { "TotalDisplay", typeof(int) },
            });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
    }
}
     