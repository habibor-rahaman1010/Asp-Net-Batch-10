using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Infrastructure.RazorUtility
{
    public class Utility
    {
        public static IList<SelectListItem> ConvertCategories(IList<Category> categories)
        {
            var Items = (from c in categories
                         select new SelectListItem(c.CategoryName, c.Id.ToString()))
                          .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertProductTypes(IList<ProductType> productTypes)
        {
            var Items = (from c in productTypes
                         select new SelectListItem(c.ProductTypeName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSubCatrgory(IList<SubCategory> subcategories)
        {
            var Items = (from c in subcategories
                         select new SelectListItem(c.SubCategoryName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertBarcodeTypes(IList<BarcodeType> productTypes)
        {
            var Items = (from c in productTypes
                         select new SelectListItem(c.BarcodeTypeName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertUnits(IList<Unit> units)
        {
            var Items = (from c in units
                         select new SelectListItem(c.UnitName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertBrands(IList<Brand> brands)
        {
            var Items = (from c in brands
                         select new SelectListItem(c.BrandName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }
        

        public static IList<SelectListItem> ConvertjustmentTypes(IList<AdjustmentType> adjustmentTypes)
        {
            var Items = (from c in adjustmentTypes
                         select new SelectListItem(c.AdjustmentTypeName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));
                
            return Items;
        }

        public static IList<SelectListItem> ConvertWarranties(IList<Warranty> warranties)
        {
            var Items = (from c in warranties
                         select new SelectListItem(c.WarrantyDuration, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertApplicableTaxes(IList<ApplicableTax> applicableTaxes)
        {
            var Items = (from c in applicableTaxes
                         select new SelectListItem(c.ApplicableTaxName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSellingPriceTaxes(IList<SellingPriceTax> sellingPriceTaxes)
        {
            var Items = (from c in sellingPriceTaxes
                         select new SelectListItem(c.SellingPriceTaxName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertBusinessLocations(IList<BusinessLocation> businessLocations)
        {
            var Items = (from c in businessLocations
                         select new SelectListItem(c.LocationName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSelectItemToProduct(IList<Product> products)
        {
            var Items = (from c in products
                         select new SelectListItem(c.ProductName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Product Select --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertCustomers(IList<Customer> customers)
        {
            var Items = (from c in customers
                         select new SelectListItem($"{c.CustomerName} ({c.CustomerCode})", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Customer --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSuppliers(IList<Supplier> suppliers)
        {
            var Items = (from c in suppliers
                         select new SelectListItem($"{c.SupplierName} ({c.SupplierCode})", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Supplier --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertPurchaseRequisitions(IList<PurchaseRequisition> purchaseRequisitions)
        {
            var Items = (from c in purchaseRequisitions
                         select new SelectListItem($"{c.RequisitionNo} - {c.BusinessLocation?.LocationName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- No Requisition (direct order) --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertPurchaseOrders(IList<PurchaseOrder> purchaseOrders)
        {
            var Items = (from c in purchaseOrders
                         select new SelectListItem($"{c.PurchaseOrderNo} - {c.Supplier?.SupplierName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Purchase Order --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertRequestForQuotations(IList<RequestForQuotation> requests)
        {
            var Items = (from c in requests
                         select new SelectListItem($"{c.RfqNo} - {c.BusinessLocation?.LocationName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Request For Quotation --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSupplierQuotations(IList<SupplierQuotation> quotations)
        {
            var Items = (from c in quotations
                         select new SelectListItem($"{c.QuotationNo} - {c.Supplier?.SupplierName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- No Quotation (direct order) --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertGoodsReceipts(IList<GoodsReceipt> goodsReceipts)
        {
            var Items = (from c in goodsReceipts
                         select new SelectListItem($"{c.GoodsReceiptNo} - {c.Supplier?.SupplierName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Goods Receipt --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertPaymentTerms(IList<PaymentTerm> paymentTerms)
        {
            var Items = (from c in paymentTerms
                         select new SelectListItem(c.TermName, c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Payment Term --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertProformaInvoices(IList<ProformaInvoice> proformaInvoices)
        {
            var Items = (from c in proformaInvoices
                         select new SelectListItem($"{c.ProformaNo} - {c.Customer?.CustomerName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Proforma Invoice --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSalespersons(IList<Salesperson> salespersons)
        {
            var Items = (from c in salespersons
                         select new SelectListItem($"{c.SalespersonCode} - {c.SalespersonName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Salesperson --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSalesQuotations(IList<SalesQuotation> salesQuotations)
        {
            var Items = (from c in salesQuotations
                         select new SelectListItem($"{c.QuotationNo} - {c.Customer?.CustomerName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Sales Quotation --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSalesOrders(IList<SalesOrder> salesOrders)
        {
            var Items = (from c in salesOrders
                         select new SelectListItem($"{c.SalesOrderNo} - {c.Customer?.CustomerName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Sales Order --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertDeliveries(IList<Delivery> deliveries)
        {
            var Items = (from c in deliveries
                         select new SelectListItem($"{c.DeliveryNo} - {c.DeliveryDate:dd-MM-yyyy}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Whole order, no single shipment --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertSalesInvoices(IList<SalesInvoice> salesInvoices)
        {
            var Items = (from c in salesInvoices
                         select new SelectListItem($"{c.InvoiceNo} - {c.Customer?.CustomerName}", c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Sales Invoice --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertCreditNotes(IList<CreditNote> creditNotes)
        {
            var Items = (from c in creditNotes
                         select new SelectListItem(
                             $"{c.CreditNoteNo} - {(c.TotalAmount - c.AppliedAmount):N2} left",
                             c.Id.ToString()))
                         .ToList();

            Items.Insert(0, new SelectListItem("-- Select Credit Note --", string.Empty));

            return Items;
        }

        public static IList<SelectListItem> ConvertEnumToSelectList<TEnum>() where TEnum : Enum
        {
            var items = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(),
                    Text = e.ToString()
                })
                .ToList();

            return items;
        }

    }
}
