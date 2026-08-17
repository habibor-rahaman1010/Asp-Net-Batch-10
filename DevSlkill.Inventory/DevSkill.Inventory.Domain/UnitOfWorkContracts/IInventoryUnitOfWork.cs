using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.UnitOfWorkContracts
{
    public interface IInventoryUnitOfWork : IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IBarcodeTypeRepository BarcodeTypeRepository { get; }
        IUnitRepository UnitRepository { get; }
        IBrandRepository BrandRepository { get; }
        ISubCategoryRepository SubCategoryRepository { get; }
        IBusinessLocationRepository BusinessLocationRepository { get; }
        IWarrantyRepository WarrantyRepository { get; }
        IApplicableTaxRepository ApplicableTaxRepository { get; }
        ISellingPriceTaxRepository SellingPriceTaxRepository { get; }
        IAdjustmentTypeRepository AdjustmentTypeRepository { get; }
        IStockAdjustmentRepository StockAdjustmentRepository { get; }
        IStockAdjustmentItemRepository StockAdjustmentItemRepository { get; }
        IUserActivityRepository UserActivityRepository { get; }
        IStockTransferRepository StockTransferRepository { get;}
        IStockTransferItemRepository StockTransferItemRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IPaymentTermRepository PaymentTermRepository { get; }
        IPriceListRepository PriceListRepository { get; }
        IPriceListItemRepository PriceListItemRepository { get; }
        IDiscountRuleRepository DiscountRuleRepository { get; }
        IProformaInvoiceRepository ProformaInvoiceRepository { get; }
        IProformaInvoiceItemRepository ProformaInvoiceItemRepository { get; }
        IDeliveryRepository DeliveryRepository { get; }
        IDeliveryItemRepository DeliveryItemRepository { get; }

        //Sales Management order-to-cash repositories
        ISalespersonRepository SalespersonRepository { get; }
        ISalesQuotationRepository SalesQuotationRepository { get; }
        ISalesQuotationItemRepository SalesQuotationItemRepository { get; }
        ISalesOrderRepository SalesOrderRepository { get; }
        ISalesOrderItemRepository SalesOrderItemRepository { get; }
        IStockReservationRepository StockReservationRepository { get; }
        IStockReservationItemRepository StockReservationItemRepository { get; }
        ISalesInvoiceRepository SalesInvoiceRepository { get; }
        ISalesInvoiceItemRepository SalesInvoiceItemRepository { get; }
        ICustomerPaymentRepository CustomerPaymentRepository { get; }
        ICustomerPaymentAllocationRepository CustomerPaymentAllocationRepository { get; }
        ISalesReturnRepository SalesReturnRepository { get; }
        ISalesReturnItemRepository SalesReturnItemRepository { get; }
        ICreditNoteRepository CreditNoteRepository { get; }
        ICreditNoteItemRepository CreditNoteItemRepository { get; }
        ISalesCommissionRepository SalesCommissionRepository { get; }

        //Purchase Management module repositories
        ISupplierRepository SupplierRepository { get; }
        IPurchaseRequisitionRepository PurchaseRequisitionRepository { get; }
        IPurchaseRequisitionItemRepository PurchaseRequisitionItemRepository { get; }
        IPurchaseOrderRepository PurchaseOrderRepository { get; }
        IPurchaseOrderItemRepository PurchaseOrderItemRepository { get; }
        IGoodsReceiptRepository GoodsReceiptRepository { get; }
        IGoodsReceiptItemRepository GoodsReceiptItemRepository { get; }
        IPurchaseInvoiceRepository PurchaseInvoiceRepository { get; }
        IPurchaseInvoiceItemRepository PurchaseInvoiceItemRepository { get; }
        IPurchaseReturnRepository PurchaseReturnRepository { get; }
        IPurchaseReturnItemRepository PurchaseReturnItemRepository { get; }
        ISupplierPaymentRepository SupplierPaymentRepository { get; }
        ISupplierPaymentAllocationRepository SupplierPaymentAllocationRepository { get; }
        IRequestForQuotationRepository RequestForQuotationRepository { get; }
        IRequestForQuotationItemRepository RequestForQuotationItemRepository { get; }
        ISupplierQuotationRepository SupplierQuotationRepository { get; }
        ISupplierQuotationItemRepository SupplierQuotationItemRepository { get; }
        Task<(IList<ProductDto> data, int total, int totalDisplay)> GetPagedProductUsingSPAsync(int pageIndex,
            int pageSize, ProductSearchDto search, string? order);
    }
}
