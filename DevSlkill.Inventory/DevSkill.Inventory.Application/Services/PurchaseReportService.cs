using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Reads the purchase book. Nothing here writes, so any report can be re-run and
    /// two of them over the same period have to agree.
    /// </summary>
    public class PurchaseReportService : IPurchaseReportService
    {
        private readonly IInventoryUnitOfWork _purchaseReportUnitOfWork;

        public PurchaseReportService(IInventoryUnitOfWork unitOfWork)
        {
            _purchaseReportUnitOfWork = unitOfWork;
        }

        public async Task<PurchaseReportDto> GetPurchaseReportAsync(PurchaseReportFilterDto filter)
        {
            // A period read back to front would silently return nothing, which reads
            // as "no activity" rather than as the mistake it is.
            if (filter.ToDate.Date < filter.FromDate.Date)
            {
                throw new InvalidOperationException("The end date cannot be earlier than the start date.");
            }

            var from = filter.FromDate.Date;

            // The end date is inclusive, so a document timed during that day counts.
            var to = filter.ToDate.Date.AddDays(1).AddTicks(-1);

            return filter.ReportType switch
            {
                PurchaseReportType.PurchaseSummary => await BuildPurchaseSummaryAsync(filter, from, to),
                PurchaseReportType.PurchaseDetail => await BuildPurchaseDetailAsync(filter, from, to),
                PurchaseReportType.SupplierWisePurchase => await BuildSupplierWiseAsync(filter, from, to),
                PurchaseReportType.ProductWisePurchase => await BuildProductWiseAsync(filter, from, to),
                PurchaseReportType.MonthlyPurchase => await BuildMonthlyAsync(filter, from, to),
                PurchaseReportType.WarehouseWisePurchase => await BuildWarehouseWiseAsync(filter, from, to),
                PurchaseReportType.PurchaseReturn => await BuildPurchaseReturnAsync(filter, from, to),
                PurchaseReportType.SupplierOutstanding => await BuildSupplierOutstandingAsync(filter),
                PurchaseReportType.PurchaseInvoice => await BuildPurchaseInvoiceAsync(filter, from, to),
                PurchaseReportType.PurchasePriceHistory => await BuildPriceHistoryAsync(filter, from, to),
                _ => throw new InvalidOperationException("Unknown purchase report.")
            };
        }

        /// <summary>
        /// One row per purchase order: what was ordered, how much of it arrived and
        /// how much the supplier has billed so far.
        /// </summary>
        private async Task<PurchaseReportDto> BuildPurchaseSummaryAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var orders = Filter(await _purchaseReportUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Purchase Summary",
                "One row per purchase order, with what has arrived and what has been billed against it.",
                new PurchaseReportColumnDto("Date"),
                new PurchaseReportColumnDto("Order No"),
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Warehouse"),
                new PurchaseReportColumnDto("Status"),
                new PurchaseReportColumnDto("Ordered Qty", true),
                new PurchaseReportColumnDto("Received Qty", true),
                new PurchaseReportColumnDto("Invoiced Qty", true),
                new PurchaseReportColumnDto("Order Value", true));

            decimal orderedQuantity = 0, receivedQuantity = 0, invoicedQuantity = 0, value = 0;

            foreach (var order in orders.OrderBy(x => x.OrderDate))
            {
                var lines = order.PurchaseOrderItems ?? new List<PurchaseOrderItem>();

                orderedQuantity += lines.Sum(x => x.Quantity);
                receivedQuantity += lines.Sum(x => x.ReceivedQuantity);
                invoicedQuantity += lines.Sum(x => x.InvoicedQuantity);
                value += order.GrandTotal;

                report.Rows.Add(new List<string>
                {
                    order.OrderDate.ToString("dd-MM-yyyy"),
                    order.PurchaseOrderNo,
                    order.Supplier?.SupplierName ?? string.Empty,
                    order.BusinessLocation?.LocationName ?? string.Empty,
                    order.Status.ToString(),
                    lines.Sum(x => x.Quantity).ToString("N2"),
                    lines.Sum(x => x.ReceivedQuantity).ToString("N2"),
                    lines.Sum(x => x.InvoicedQuantity).ToString("N2"),
                    order.GrandTotal.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty, $"{report.Rows.Count} orders",
                orderedQuantity.ToString("N2"),
                receivedQuantity.ToString("N2"),
                invoicedQuantity.ToString("N2"),
                value.ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// One row per purchase order line, which is the level a buyer checks a price
        /// or a quantity at.
        /// </summary>
        private async Task<PurchaseReportDto> BuildPurchaseDetailAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var orders = Filter(await _purchaseReportUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Purchase Detail",
                "One row per purchase order line.",
                new PurchaseReportColumnDto("Date"),
                new PurchaseReportColumnDto("Order No"),
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Warehouse"),
                new PurchaseReportColumnDto("Product"),
                new PurchaseReportColumnDto("Unit"),
                new PurchaseReportColumnDto("Quantity", true),
                new PurchaseReportColumnDto("Unit Price", true),
                new PurchaseReportColumnDto("Discount", true),
                new PurchaseReportColumnDto("Tax", true),
                new PurchaseReportColumnDto("Line Total", true));

            decimal quantity = 0, discount = 0, tax = 0, lineTotal = 0;

            foreach (var order in orders.OrderBy(x => x.OrderDate))
            {
                foreach (var line in (order.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter)))
                {
                    quantity += line.Quantity;
                    discount += line.DiscountAmount;
                    tax += line.TaxAmount;
                    lineTotal += line.LineTotal;

                    report.Rows.Add(new List<string>
                    {
                        order.OrderDate.ToString("dd-MM-yyyy"),
                        order.PurchaseOrderNo,
                        order.Supplier?.SupplierName ?? string.Empty,
                        order.BusinessLocation?.LocationName ?? string.Empty,
                        line.Product?.ProductName ?? string.Empty,
                        line.Product?.Unit?.UnitName ?? string.Empty,
                        line.Quantity.ToString("N2"),
                        line.UnitPrice.ToString("N2"),
                        line.DiscountAmount.ToString("N2"),
                        line.TaxAmount.ToString("N2"),
                        line.LineTotal.ToString("N2")
                    });
                }
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                $"{report.Rows.Count} lines",
                quantity.ToString("N2"),
                string.Empty,
                discount.ToString("N2"),
                tax.ToString("N2"),
                lineTotal.ToString("N2")
            };

            return report;
        }

        /// <summary>What was bought from each supplier over the period.</summary>
        private async Task<PurchaseReportDto> BuildSupplierWiseAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var orders = Filter(await _purchaseReportUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Supplier-wise Purchase",
                "What was bought from each supplier over the period.",
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Orders", true),
                new PurchaseReportColumnDto("Ordered Qty", true),
                new PurchaseReportColumnDto("Received Qty", true),
                new PurchaseReportColumnDto("Order Value", true));

            var grouped = orders
                .GroupBy(x => x.Supplier?.SupplierName ?? "(no supplier)")
                .Select(g => new
                {
                    Supplier = g.Key,
                    Orders = g.Count(),
                    Ordered = g.SelectMany(x => x.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                        .Sum(x => x.Quantity),
                    Received = g.SelectMany(x => x.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                        .Sum(x => x.ReceivedQuantity),
                    Value = g.Sum(x => x.GrandTotal)
                })
                .OrderByDescending(x => x.Value)
                .ToList();

            foreach (var row in grouped)
            {
                report.Rows.Add(new List<string>
                {
                    row.Supplier,
                    row.Orders.ToString(),
                    row.Ordered.ToString("N2"),
                    row.Received.ToString("N2"),
                    row.Value.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{grouped.Count} suppliers",
                grouped.Sum(x => x.Orders).ToString(),
                grouped.Sum(x => x.Ordered).ToString("N2"),
                grouped.Sum(x => x.Received).ToString("N2"),
                grouped.Sum(x => x.Value).ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// What was bought of each product, with the average price paid. It is read
        /// off the order lines, so it says what was agreed rather than what arrived.
        /// </summary>
        private async Task<PurchaseReportDto> BuildProductWiseAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var orders = Filter(await _purchaseReportUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Product-wise Purchase",
                "What was bought of each product, with the average price paid.",
                new PurchaseReportColumnDto("Product"),
                new PurchaseReportColumnDto("Unit"),
                new PurchaseReportColumnDto("Orders", true),
                new PurchaseReportColumnDto("Quantity", true),
                new PurchaseReportColumnDto("Avg Price", true),
                new PurchaseReportColumnDto("Line Total", true));

            var grouped = orders
                .SelectMany(order => (order.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter))
                    .Select(line => new { order, line }))
                .GroupBy(x => x.line.ProductId)
                .Select(g => new
                {
                    Product = g.First().line.Product?.ProductName ?? string.Empty,
                    Unit = g.First().line.Product?.Unit?.UnitName ?? string.Empty,
                    Orders = g.Select(x => x.order.Id).Distinct().Count(),
                    Quantity = g.Sum(x => x.line.Quantity),
                    Total = g.Sum(x => x.line.LineTotal)
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            foreach (var row in grouped)
            {
                report.Rows.Add(new List<string>
                {
                    row.Product,
                    row.Unit,
                    row.Orders.ToString(),
                    row.Quantity.ToString("N2"),
                    (row.Quantity == 0 ? 0 : row.Total / row.Quantity).ToString("N2"),
                    row.Total.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{grouped.Count} products",
                string.Empty,
                string.Empty,
                grouped.Sum(x => x.Quantity).ToString("N2"),
                string.Empty,
                grouped.Sum(x => x.Total).ToString("N2")
            };

            return report;
        }

        /// <summary>Purchases month by month, so a trend can be read off it.</summary>
        private async Task<PurchaseReportDto> BuildMonthlyAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var orders = Filter(await _purchaseReportUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Monthly Purchase",
                "Purchases month by month over the period.",
                new PurchaseReportColumnDto("Month"),
                new PurchaseReportColumnDto("Orders", true),
                new PurchaseReportColumnDto("Quantity", true),
                new PurchaseReportColumnDto("Order Value", true));

            var grouped = orders
                .GroupBy(x => new { x.OrderDate.Year, x.OrderDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Orders = g.Count(),
                    Quantity = g.SelectMany(x => x.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                        .Sum(x => x.Quantity),
                    Value = g.Sum(x => x.GrandTotal)
                })
                .OrderBy(x => x.Month)
                .ToList();

            foreach (var row in grouped)
            {
                report.Rows.Add(new List<string>
                {
                    row.Month.ToString("MMMM yyyy"),
                    row.Orders.ToString(),
                    row.Quantity.ToString("N2"),
                    row.Value.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{grouped.Count} months",
                grouped.Sum(x => x.Orders).ToString(),
                grouped.Sum(x => x.Quantity).ToString("N2"),
                grouped.Sum(x => x.Value).ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// What each warehouse actually took in. This one reads the goods receipts,
        /// because an order only promises a delivery.
        /// </summary>
        private async Task<PurchaseReportDto> BuildWarehouseWiseAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var receipts = (await _purchaseReportUnitOfWork
                .GoodsReceiptRepository
                .GetGoodsReceiptsByDateRangeAsync(from, to))
                .Where(x => x.Status == GoodsReceiptStatus.Received)
                .Where(x => !filter.SupplierId.HasValue || x.SupplierId == filter.SupplierId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.BusinessLocationId == filter.BusinessLocationId.Value)
                .ToList();

            var report = Report(filter, "Warehouse-wise Purchase",
                "What each warehouse actually received, taken from the confirmed goods receipts.",
                new PurchaseReportColumnDto("Warehouse"),
                new PurchaseReportColumnDto("Receipts", true),
                new PurchaseReportColumnDto("Received Qty", true),
                new PurchaseReportColumnDto("Rejected Qty", true),
                new PurchaseReportColumnDto("Returned Qty", true),
                new PurchaseReportColumnDto("Received Value", true));

            var grouped = receipts
                .GroupBy(x => x.BusinessLocation?.LocationName ?? "(no warehouse)")
                .Select(g => new
                {
                    Warehouse = g.Key,
                    Receipts = g.Count(),
                    Received = Lines(g).Sum(x => x.ReceivedQuantity),
                    Rejected = Lines(g).Sum(x => x.RejectedQuantity),
                    Returned = Lines(g).Sum(x => x.ReturnedQuantity),
                    Value = Lines(g).Sum(x => x.ReceivedQuantity * x.UnitPrice)
                })
                .OrderByDescending(x => x.Value)
                .ToList();

            foreach (var row in grouped)
            {
                report.Rows.Add(new List<string>
                {
                    row.Warehouse,
                    row.Receipts.ToString(),
                    row.Received.ToString("N2"),
                    row.Rejected.ToString("N2"),
                    row.Returned.ToString("N2"),
                    row.Value.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{grouped.Count} warehouses",
                grouped.Sum(x => x.Receipts).ToString(),
                grouped.Sum(x => x.Received).ToString("N2"),
                grouped.Sum(x => x.Rejected).ToString("N2"),
                grouped.Sum(x => x.Returned).ToString("N2"),
                grouped.Sum(x => x.Value).ToString("N2")
            };

            return report;

            IEnumerable<GoodsReceiptItem> Lines(IEnumerable<GoodsReceipt> source)
            {
                return source
                    .SelectMany(x => x.GoodsReceiptItems ?? new List<GoodsReceiptItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter));
            }
        }

        /// <summary>What went back to the suppliers, line by line.</summary>
        private async Task<PurchaseReportDto> BuildPurchaseReturnAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var returns = (await _purchaseReportUnitOfWork
                .PurchaseReturnRepository
                .GetPurchaseReturnsByDateRangeAsync(from, to))
                .Where(x => x.Status == PurchaseReturnStatus.Returned)
                .Where(x => !filter.SupplierId.HasValue || x.SupplierId == filter.SupplierId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.BusinessLocationId == filter.BusinessLocationId.Value)
                .ToList();

            var report = Report(filter, "Purchase Return",
                "Goods sent back to suppliers, taken from the confirmed returns.",
                new PurchaseReportColumnDto("Date"),
                new PurchaseReportColumnDto("Return No"),
                new PurchaseReportColumnDto("Receipt No"),
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Warehouse"),
                new PurchaseReportColumnDto("Product"),
                new PurchaseReportColumnDto("Reason"),
                new PurchaseReportColumnDto("Quantity", true),
                new PurchaseReportColumnDto("Unit Price", true),
                new PurchaseReportColumnDto("Credit", true));

            decimal quantity = 0, credit = 0;

            foreach (var purchaseReturn in returns.OrderBy(x => x.ReturnDate))
            {
                foreach (var line in (purchaseReturn.PurchaseReturnItems ?? new List<PurchaseReturnItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter)))
                {
                    quantity += line.ReturnQuantity;
                    credit += line.LineTotal;

                    report.Rows.Add(new List<string>
                    {
                        purchaseReturn.ReturnDate.ToString("dd-MM-yyyy"),
                        purchaseReturn.ReturnNo,
                        purchaseReturn.GoodsReceipt?.GoodsReceiptNo ?? string.Empty,
                        purchaseReturn.Supplier?.SupplierName ?? string.Empty,
                        purchaseReturn.BusinessLocation?.LocationName ?? string.Empty,
                        line.Product?.ProductName ?? string.Empty,
                        purchaseReturn.Reason,
                        line.ReturnQuantity.ToString("N2"),
                        line.UnitPrice.ToString("N2"),
                        line.LineTotal.ToString("N2")
                    });
                }
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                $"{report.Rows.Count} lines",
                quantity.ToString("N2"),
                string.Empty,
                credit.ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// What each supplier is owed right now. It is a position rather than a
        /// period, so the dates do not narrow it.
        /// </summary>
        private async Task<PurchaseReportDto> BuildSupplierOutstandingAsync(PurchaseReportFilterDto filter)
        {
            var suppliers = (await _purchaseReportUnitOfWork.SupplierRepository.GetAllAsync())
                .Where(x => !filter.SupplierId.HasValue || x.Id == filter.SupplierId.Value)
                .ToList();

            var report = Report(filter, "Supplier Outstanding",
                "What each supplier is owed as things stand today, so the dates above do not narrow it.",
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Code"),
                new PurchaseReportColumnDto("Phone"),
                new PurchaseReportColumnDto("Status"),
                new PurchaseReportColumnDto("Opening", true),
                new PurchaseReportColumnDto("Credit Limit", true),
                new PurchaseReportColumnDto("Outstanding", true));

            var outstanding = suppliers
                .Where(x => x.CurrentOutstanding != 0)
                .OrderByDescending(x => x.CurrentOutstanding)
                .ToList();

            foreach (var supplier in outstanding)
            {
                report.Rows.Add(new List<string>
                {
                    supplier.SupplierName,
                    supplier.SupplierCode,
                    supplier.Phone,
                    supplier.Status.ToString(),
                    supplier.OpeningBalance.ToString("N2"),
                    supplier.CreditLimit.ToString("N2"),
                    supplier.CurrentOutstanding.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{outstanding.Count} suppliers",
                string.Empty, string.Empty, string.Empty,
                outstanding.Sum(x => x.OpeningBalance).ToString("N2"),
                outstanding.Sum(x => x.CreditLimit).ToString("N2"),
                outstanding.Sum(x => x.CurrentOutstanding).ToString("N2")
            };

            return report;
        }

        /// <summary>Every invoice raised in the period, with what is still due on it.</summary>
        private async Task<PurchaseReportDto> BuildPurchaseInvoiceAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var invoices = (await _purchaseReportUnitOfWork
                .PurchaseInvoiceRepository
                .GetPurchaseInvoicesByDateRangeAsync(from, to))
                .Where(x => !filter.SupplierId.HasValue || x.SupplierId == filter.SupplierId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.PurchaseOrder?.BusinessLocationId == filter.BusinessLocationId.Value)
                .ToList();

            var report = Report(filter, "Purchase Invoice",
                "Every invoice raised in the period, with what is still due on it.",
                new PurchaseReportColumnDto("Date"),
                new PurchaseReportColumnDto("Invoice No"),
                new PurchaseReportColumnDto("Supplier Bill"),
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Due Date"),
                new PurchaseReportColumnDto("Status"),
                new PurchaseReportColumnDto("Sub Total", true),
                new PurchaseReportColumnDto("Tax", true),
                new PurchaseReportColumnDto("Grand Total", true),
                new PurchaseReportColumnDto("Paid", true),
                new PurchaseReportColumnDto("Due", true));

            foreach (var invoice in invoices.OrderBy(x => x.InvoiceDate))
            {
                report.Rows.Add(new List<string>
                {
                    invoice.InvoiceDate.ToString("dd-MM-yyyy"),
                    invoice.InvoiceNo,
                    invoice.SupplierInvoiceNo,
                    invoice.Supplier?.SupplierName ?? string.Empty,
                    invoice.DueDate.ToString("dd-MM-yyyy"),
                    invoice.Status.ToString(),
                    invoice.SubTotal.ToString("N2"),
                    invoice.TotalTax.ToString("N2"),
                    invoice.GrandTotal.ToString("N2"),
                    invoice.PaidAmount.ToString("N2"),
                    invoice.DueAmount.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                $"{invoices.Count} invoices",
                invoices.Sum(x => x.SubTotal).ToString("N2"),
                invoices.Sum(x => x.TotalTax).ToString("N2"),
                invoices.Sum(x => x.GrandTotal).ToString("N2"),
                invoices.Sum(x => x.PaidAmount).ToString("N2"),
                invoices.Sum(x => x.DueAmount).ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// What each product cost, order by order. Reading it down a product shows
        /// whether a supplier's price is moving.
        /// </summary>
        private async Task<PurchaseReportDto> BuildPriceHistoryAsync(PurchaseReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var orders = Filter(await _purchaseReportUnitOfWork
                .PurchaseOrderRepository
                .GetPurchaseOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Purchase Price History",
                "What each product cost, order by order, so a moving price stands out.",
                new PurchaseReportColumnDto("Product"),
                new PurchaseReportColumnDto("Unit"),
                new PurchaseReportColumnDto("Date"),
                new PurchaseReportColumnDto("Order No"),
                new PurchaseReportColumnDto("Supplier"),
                new PurchaseReportColumnDto("Quantity", true),
                new PurchaseReportColumnDto("Unit Price", true),
                new PurchaseReportColumnDto("Change", true));

            var history = orders
                .SelectMany(order => (order.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter))
                    .Select(line => new
                    {
                        line.ProductId,
                        Product = line.Product?.ProductName ?? string.Empty,
                        Unit = line.Product?.Unit?.UnitName ?? string.Empty,
                        order.OrderDate,
                        order.PurchaseOrderNo,
                        Supplier = order.Supplier?.SupplierName ?? string.Empty,
                        line.Quantity,
                        line.UnitPrice
                    }))
                .OrderBy(x => x.Product)
                .ThenBy(x => x.OrderDate)
                .ToList();

            decimal? previousPrice = null;
            Guid? previousProductId = null;

            foreach (var row in history)
            {
                // The change is only meaningful against the same product's last price,
                // so it restarts whenever the product does.
                var change = previousProductId == row.ProductId && previousPrice.HasValue
                    ? row.UnitPrice - previousPrice.Value
                    : (decimal?)null;

                report.Rows.Add(new List<string>
                {
                    row.Product,
                    row.Unit,
                    row.OrderDate.ToString("dd-MM-yyyy"),
                    row.PurchaseOrderNo,
                    row.Supplier,
                    row.Quantity.ToString("N2"),
                    row.UnitPrice.ToString("N2"),
                    change.HasValue ? change.Value.ToString("+0.00;-0.00;0.00") : "—"
                });

                previousPrice = row.UnitPrice;
                previousProductId = row.ProductId;
            }

            report.Totals = new List<string>
            {
                $"{history.Select(x => x.ProductId).Distinct().Count()} products",
                string.Empty, string.Empty, string.Empty,
                $"{report.Rows.Count} lines",
                history.Sum(x => x.Quantity).ToString("N2"),
                string.Empty,
                string.Empty
            };

            return report;
        }

        private static PurchaseReportDto Report(PurchaseReportFilterDto filter, string title, string description,
            params PurchaseReportColumnDto[] columns)
        {
            return new PurchaseReportDto
            {
                Title = title,
                Description = description,
                FromDate = filter.FromDate.Date,
                ToDate = filter.ToDate.Date,
                Columns = columns.ToList()
            };
        }

        private static List<PurchaseOrder> Filter(IEnumerable<PurchaseOrder> orders, PurchaseReportFilterDto filter)
        {
            return orders
                .Where(x => !filter.SupplierId.HasValue || x.SupplierId == filter.SupplierId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.BusinessLocationId == filter.BusinessLocationId.Value)
                .Where(x => !filter.ProductId.HasValue
                    || (x.PurchaseOrderItems ?? new List<PurchaseOrderItem>())
                        .Any(line => line.ProductId == filter.ProductId.Value))
                .ToList();
        }

        private static bool MatchesProduct(Guid productId, PurchaseReportFilterDto filter)
        {
            return !filter.ProductId.HasValue || productId == filter.ProductId.Value;
        }
    }
}
