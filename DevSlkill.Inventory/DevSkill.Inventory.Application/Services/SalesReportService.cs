using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    /// <summary>
    /// Reads the sales book. Nothing here writes, so any report can be re-run and two
    /// of them over the same period have to agree.
    /// </summary>
    public class SalesReportService : ISalesReportService
    {
        private readonly IInventoryUnitOfWork _salesReportUnitOfWork;

        /// <summary>Only an invoice that was really claimed counts as sales.</summary>
        private static readonly SalesInvoiceStatus[] CountableInvoiceStatuses =
        {
            SalesInvoiceStatus.Posted,
            SalesInvoiceStatus.PartiallyPaid,
            SalesInvoiceStatus.Paid
        };

        public SalesReportService(IInventoryUnitOfWork unitOfWork)
        {
            _salesReportUnitOfWork = unitOfWork;
        }

        public async Task<SalesReportDto> GetSalesReportAsync(SalesReportFilterDto filter)
        {
            // A period read back to front would silently return nothing, which reads as
            // "no activity" rather than as the mistake it is.
            if (filter.ToDate.Date < filter.FromDate.Date)
            {
                throw new InvalidOperationException("The end date cannot be earlier than the start date.");
            }

            var from = filter.FromDate.Date;

            // The end date is inclusive, so a document timed during that day counts.
            var to = filter.ToDate.Date.AddDays(1).AddTicks(-1);

            return filter.ReportType switch
            {
                SalesReportType.SalesSummary => await BuildSalesSummaryAsync(filter, from, to),
                SalesReportType.SalesDetail => await BuildSalesDetailAsync(filter, from, to),
                SalesReportType.CustomerWiseSales => await BuildCustomerWiseAsync(filter, from, to),
                SalesReportType.ProductWiseSales => await BuildProductWiseAsync(filter, from, to),
                SalesReportType.MonthlySales => await BuildMonthlyAsync(filter, from, to),
                SalesReportType.WarehouseWiseSales => await BuildWarehouseWiseAsync(filter, from, to),
                SalesReportType.SalesReturn => await BuildSalesReturnAsync(filter, from, to),
                SalesReportType.CustomerOutstanding => await BuildCustomerOutstandingAsync(filter),
                SalesReportType.SalesInvoice => await BuildSalesInvoiceAsync(filter, from, to),
                SalesReportType.SalespersonPerformance => await BuildSalespersonPerformanceAsync(filter, from, to),
                _ => throw new InvalidOperationException("Unknown sales report.")
            };
        }

        /// <summary>
        /// One row per sales order: what was sold, how much of it has shipped and how
        /// much has been billed so far.
        /// </summary>
        private async Task<SalesReportDto> BuildSalesSummaryAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var orders = Filter(await _salesReportUnitOfWork
                .SalesOrderRepository
                .GetSalesOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Sales Summary",
                "One row per sales order, with what has shipped and what has been billed against it.",
                new SalesReportColumnDto("Date"),
                new SalesReportColumnDto("Order No"),
                new SalesReportColumnDto("Customer"),
                new SalesReportColumnDto("Warehouse"),
                new SalesReportColumnDto("Status"),
                new SalesReportColumnDto("Ordered Qty", true),
                new SalesReportColumnDto("Delivered Qty", true),
                new SalesReportColumnDto("Invoiced Qty", true),
                new SalesReportColumnDto("Order Value", true));

            decimal orderedQuantity = 0, deliveredQuantity = 0, invoicedQuantity = 0, value = 0;

            foreach (var order in orders.OrderBy(x => x.OrderDate))
            {
                var lines = order.SalesOrderItems ?? new List<SalesOrderItem>();

                orderedQuantity += lines.Sum(x => x.Quantity);
                deliveredQuantity += lines.Sum(x => x.DeliveredQuantity);
                invoicedQuantity += lines.Sum(x => x.InvoicedQuantity);
                value += order.GrandTotal;

                report.Rows.Add(new List<string>
                {
                    order.OrderDate.ToString("dd-MM-yyyy"),
                    order.SalesOrderNo,
                    order.Customer?.CustomerName ?? string.Empty,
                    order.BusinessLocation?.LocationName ?? string.Empty,
                    order.Status.ToString(),
                    lines.Sum(x => x.Quantity).ToString("N2"),
                    lines.Sum(x => x.DeliveredQuantity).ToString("N2"),
                    lines.Sum(x => x.InvoicedQuantity).ToString("N2"),
                    order.GrandTotal.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty, $"{report.Rows.Count} orders",
                orderedQuantity.ToString("N2"),
                deliveredQuantity.ToString("N2"),
                invoicedQuantity.ToString("N2"),
                value.ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// One row per sales order line, which is the level a price or a quantity is
        /// actually checked at.
        /// </summary>
        private async Task<SalesReportDto> BuildSalesDetailAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var orders = Filter(await _salesReportUnitOfWork
                .SalesOrderRepository
                .GetSalesOrdersByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Sales Detail",
                "One row per sales order line.",
                new SalesReportColumnDto("Date"),
                new SalesReportColumnDto("Order No"),
                new SalesReportColumnDto("Customer"),
                new SalesReportColumnDto("Warehouse"),
                new SalesReportColumnDto("Product"),
                new SalesReportColumnDto("Unit"),
                new SalesReportColumnDto("Qty", true),
                new SalesReportColumnDto("Unit Price", true),
                new SalesReportColumnDto("Discount", true),
                new SalesReportColumnDto("Tax", true),
                new SalesReportColumnDto("Line Total", true));

            decimal quantity = 0, discount = 0, tax = 0, total = 0;

            foreach (var order in orders.OrderBy(x => x.OrderDate))
            {
                foreach (var line in (order.SalesOrderItems ?? new List<SalesOrderItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter)))
                {
                    quantity += line.Quantity;
                    discount += line.DiscountAmount;
                    tax += line.TaxAmount;
                    total += line.LineTotal;

                    report.Rows.Add(new List<string>
                    {
                        order.OrderDate.ToString("dd-MM-yyyy"),
                        order.SalesOrderNo,
                        order.Customer?.CustomerName ?? string.Empty,
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
                total.ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// One row per customer, read off the invoices, because what was actually
        /// billed is what a customer is worth.
        /// </summary>
        private async Task<SalesReportDto> BuildCustomerWiseAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var invoices = FilterInvoices(await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Customer Wise Sales",
                "One row per customer, taken from the invoices raised in the period.",
                new SalesReportColumnDto("Customer"),
                new SalesReportColumnDto("Code"),
                new SalesReportColumnDto("Invoices", true),
                new SalesReportColumnDto("Qty", true),
                new SalesReportColumnDto("Discount", true),
                new SalesReportColumnDto("Tax", true),
                new SalesReportColumnDto("Sales Value", true),
                new SalesReportColumnDto("Collected", true),
                new SalesReportColumnDto("Due", true));

            var groups = invoices
                .GroupBy(x => new
                {
                    x.CustomerId,
                    Name = x.Customer?.CustomerName ?? string.Empty,
                    Code = x.Customer?.CustomerCode ?? string.Empty
                })
                .OrderByDescending(x => x.Sum(i => i.GrandTotal));

            decimal quantity = 0, discount = 0, tax = 0, value = 0, collected = 0, due = 0;
            var invoiceCount = 0;

            foreach (var group in groups)
            {
                var groupQuantity = group.Sum(x => (x.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                    .Sum(l => l.Quantity));

                quantity += groupQuantity;
                discount += group.Sum(x => x.ItemDiscountTotal);
                tax += group.Sum(x => x.TotalTax);
                value += group.Sum(x => x.GrandTotal);
                collected += group.Sum(x => x.PaidAmount);
                due += group.Sum(x => x.DueAmount);
                invoiceCount += group.Count();

                report.Rows.Add(new List<string>
                {
                    group.Key.Name,
                    group.Key.Code,
                    group.Count().ToString("N0"),
                    groupQuantity.ToString("N2"),
                    group.Sum(x => x.ItemDiscountTotal).ToString("N2"),
                    group.Sum(x => x.TotalTax).ToString("N2"),
                    group.Sum(x => x.GrandTotal).ToString("N2"),
                    group.Sum(x => x.PaidAmount).ToString("N2"),
                    group.Sum(x => x.DueAmount).ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{report.Rows.Count} customers",
                string.Empty,
                invoiceCount.ToString("N0"),
                quantity.ToString("N2"),
                discount.ToString("N2"),
                tax.ToString("N2"),
                value.ToString("N2"),
                collected.ToString("N2"),
                due.ToString("N2")
            };

            return report;
        }

        /// <summary>One row per product, so the fast and slow movers stand out.</summary>
        private async Task<SalesReportDto> BuildProductWiseAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var invoices = FilterInvoices(await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Product Wise Sales",
                "One row per product, taken from the invoice lines raised in the period.",
                new SalesReportColumnDto("Product"),
                new SalesReportColumnDto("Unit"),
                new SalesReportColumnDto("Invoices", true),
                new SalesReportColumnDto("Qty Sold", true),
                new SalesReportColumnDto("Qty Returned", true),
                new SalesReportColumnDto("Avg Price", true),
                new SalesReportColumnDto("Sales Value", true));

            var lines = invoices
                .SelectMany(invoice => (invoice.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                    .Where(line => MatchesProduct(line.ProductId, filter))
                    .Select(line => new { invoice.Id, line }))
                .ToList();

            decimal quantity = 0, returned = 0, value = 0;

            foreach (var group in lines
                .GroupBy(x => new
                {
                    x.line.ProductId,
                    Product = x.line.Product?.ProductName ?? string.Empty,
                    Unit = x.line.Product?.Unit?.UnitName ?? string.Empty
                })
                .OrderByDescending(x => x.Sum(l => l.line.LineTotal)))
            {
                var groupQuantity = group.Sum(x => x.line.Quantity);
                var groupReturned = group.Sum(x => x.line.ReturnedQuantity);
                var groupValue = group.Sum(x => x.line.LineTotal);

                quantity += groupQuantity;
                returned += groupReturned;
                value += groupValue;

                report.Rows.Add(new List<string>
                {
                    group.Key.Product,
                    group.Key.Unit,
                    group.Select(x => x.Id).Distinct().Count().ToString("N0"),
                    groupQuantity.ToString("N2"),
                    groupReturned.ToString("N2"),
                    (groupQuantity > 0 ? groupValue / groupQuantity : 0m).ToString("N2"),
                    groupValue.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{report.Rows.Count} products",
                string.Empty,
                lines.Select(x => x.Id).Distinct().Count().ToString("N0"),
                quantity.ToString("N2"),
                returned.ToString("N2"),
                string.Empty,
                value.ToString("N2")
            };

            return report;
        }

        /// <summary>One row per month, which is how a trend is actually read.</summary>
        private async Task<SalesReportDto> BuildMonthlyAsync(SalesReportFilterDto filter, DateTime from, DateTime to)
        {
            var invoices = FilterInvoices(await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Monthly Sales",
                "One row per month, taken from the invoices raised in the period.",
                new SalesReportColumnDto("Month"),
                new SalesReportColumnDto("Invoices", true),
                new SalesReportColumnDto("Qty", true),
                new SalesReportColumnDto("Discount", true),
                new SalesReportColumnDto("Tax", true),
                new SalesReportColumnDto("Sales Value", true),
                new SalesReportColumnDto("Collected", true));

            decimal quantity = 0, discount = 0, tax = 0, value = 0, collected = 0;
            var invoiceCount = 0;

            foreach (var group in invoices
                .GroupBy(x => new { x.InvoiceDate.Year, x.InvoiceDate.Month })
                .OrderBy(x => x.Key.Year)
                .ThenBy(x => x.Key.Month))
            {
                var groupQuantity = group.Sum(x => (x.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                    .Sum(l => l.Quantity));

                quantity += groupQuantity;
                discount += group.Sum(x => x.ItemDiscountTotal);
                tax += group.Sum(x => x.TotalTax);
                value += group.Sum(x => x.GrandTotal);
                collected += group.Sum(x => x.PaidAmount);
                invoiceCount += group.Count();

                report.Rows.Add(new List<string>
                {
                    new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yyyy"),
                    group.Count().ToString("N0"),
                    groupQuantity.ToString("N2"),
                    group.Sum(x => x.ItemDiscountTotal).ToString("N2"),
                    group.Sum(x => x.TotalTax).ToString("N2"),
                    group.Sum(x => x.GrandTotal).ToString("N2"),
                    group.Sum(x => x.PaidAmount).ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{report.Rows.Count} months",
                invoiceCount.ToString("N0"),
                quantity.ToString("N2"),
                discount.ToString("N2"),
                tax.ToString("N2"),
                value.ToString("N2"),
                collected.ToString("N2")
            };

            return report;
        }

        /// <summary>One row per warehouse, so the branches can be compared.</summary>
        private async Task<SalesReportDto> BuildWarehouseWiseAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var invoices = FilterInvoices(await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByDateRangeAsync(from, to), filter);

            var report = Report(filter, "Warehouse Wise Sales",
                "One row per warehouse, taken from the invoices raised in the period.",
                new SalesReportColumnDto("Warehouse"),
                new SalesReportColumnDto("Invoices", true),
                new SalesReportColumnDto("Customers", true),
                new SalesReportColumnDto("Qty", true),
                new SalesReportColumnDto("Sales Value", true),
                new SalesReportColumnDto("Collected", true),
                new SalesReportColumnDto("Due", true));

            decimal quantity = 0, value = 0, collected = 0, due = 0;
            var invoiceCount = 0;

            foreach (var group in invoices
                .GroupBy(x => x.BusinessLocation?.LocationName ?? string.Empty)
                .OrderByDescending(x => x.Sum(i => i.GrandTotal)))
            {
                var groupQuantity = group.Sum(x => (x.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                    .Sum(l => l.Quantity));

                quantity += groupQuantity;
                value += group.Sum(x => x.GrandTotal);
                collected += group.Sum(x => x.PaidAmount);
                due += group.Sum(x => x.DueAmount);
                invoiceCount += group.Count();

                report.Rows.Add(new List<string>
                {
                    group.Key,
                    group.Count().ToString("N0"),
                    group.Select(x => x.CustomerId).Distinct().Count().ToString("N0"),
                    groupQuantity.ToString("N2"),
                    group.Sum(x => x.GrandTotal).ToString("N2"),
                    group.Sum(x => x.PaidAmount).ToString("N2"),
                    group.Sum(x => x.DueAmount).ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{report.Rows.Count} warehouses",
                invoiceCount.ToString("N0"),
                invoices.Select(x => x.CustomerId).Distinct().Count().ToString("N0"),
                quantity.ToString("N2"),
                value.ToString("N2"),
                collected.ToString("N2"),
                due.ToString("N2")
            };

            return report;
        }

        /// <summary>One row per returned line, with the reason it came back.</summary>
        private async Task<SalesReportDto> BuildSalesReturnAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var returns = (await _salesReportUnitOfWork
                .SalesReturnRepository
                .GetSalesReturnsByDateRangeAsync(from, to))
                .Where(x => x.Status == SalesReturnStatus.Returned)
                .Where(x => !filter.CustomerId.HasValue || x.CustomerId == filter.CustomerId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.BusinessLocationId == filter.BusinessLocationId.Value)
                .ToList();

            var report = Report(filter, "Sales Return",
                "One row per returned line, taken from the returns confirmed in the period.",
                new SalesReportColumnDto("Date"),
                new SalesReportColumnDto("Return No"),
                new SalesReportColumnDto("Customer"),
                new SalesReportColumnDto("Product"),
                new SalesReportColumnDto("Unit"),
                new SalesReportColumnDto("Qty", true),
                new SalesReportColumnDto("Unit Price", true),
                new SalesReportColumnDto("Line Total", true),
                new SalesReportColumnDto("Reason"));

            decimal quantity = 0, value = 0;

            foreach (var salesReturn in returns.OrderBy(x => x.ReturnDate))
            {
                foreach (var line in (salesReturn.SalesReturnItems ?? new List<SalesReturnItem>())
                    .Where(x => MatchesProduct(x.ProductId, filter)))
                {
                    quantity += line.ReturnQuantity;
                    value += line.LineTotal;

                    report.Rows.Add(new List<string>
                    {
                        salesReturn.ReturnDate.ToString("dd-MM-yyyy"),
                        salesReturn.ReturnNo,
                        salesReturn.Customer?.CustomerName ?? string.Empty,
                        line.Product?.ProductName ?? string.Empty,
                        line.Product?.Unit?.UnitName ?? string.Empty,
                        line.ReturnQuantity.ToString("N2"),
                        line.UnitPrice.ToString("N2"),
                        line.LineTotal.ToString("N2"),
                        string.IsNullOrWhiteSpace(line.Remarks) ? salesReturn.Reason : line.Remarks
                    });
                }
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty,
                $"{report.Rows.Count} lines",
                quantity.ToString("N2"),
                string.Empty,
                value.ToString("N2"),
                string.Empty
            };

            return report;
        }

        /// <summary>
        /// What every customer owes right now, oldest debt first. This one ignores the
        /// period: a balance is a fact of today, not of a date range.
        /// </summary>
        private async Task<SalesReportDto> BuildCustomerOutstandingAsync(SalesReportFilterDto filter)
        {
            var invoices = (await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByStatusAsync(SalesInvoiceStatus.Posted, SalesInvoiceStatus.PartiallyPaid))
                .Where(x => !filter.CustomerId.HasValue || x.CustomerId == filter.CustomerId.Value)
                .Where(x => x.DueAmount > 0)
                .ToList();

            var report = Report(filter, "Customer Outstanding",
                "Every invoice still owing money, as it stands today.",
                new SalesReportColumnDto("Customer"),
                new SalesReportColumnDto("Invoice No"),
                new SalesReportColumnDto("Invoice Date"),
                new SalesReportColumnDto("Due Date"),
                new SalesReportColumnDto("Overdue Days", true),
                new SalesReportColumnDto("Invoice Value", true),
                new SalesReportColumnDto("Collected", true),
                new SalesReportColumnDto("Outstanding", true));

            var today = DateTime.Now.Date;
            decimal value = 0, collected = 0, outstanding = 0;

            foreach (var invoice in invoices
                .OrderBy(x => x.DueDate)
                .ThenBy(x => x.InvoiceDate))
            {
                var overdueDays = (int)(today - invoice.DueDate.Date).TotalDays;

                value += invoice.GrandTotal;
                collected += invoice.PaidAmount;
                outstanding += invoice.DueAmount;

                report.Rows.Add(new List<string>
                {
                    invoice.Customer?.CustomerName ?? string.Empty,
                    invoice.InvoiceNo,
                    invoice.InvoiceDate.ToString("dd-MM-yyyy"),
                    invoice.DueDate.ToString("dd-MM-yyyy"),
                    overdueDays > 0 ? overdueDays.ToString("N0") : "—",
                    invoice.GrandTotal.ToString("N2"),
                    invoice.PaidAmount.ToString("N2"),
                    invoice.DueAmount.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{invoices.Select(x => x.CustomerId).Distinct().Count()} customers",
                $"{report.Rows.Count} invoices",
                string.Empty, string.Empty, string.Empty,
                value.ToString("N2"),
                collected.ToString("N2"),
                outstanding.ToString("N2")
            };

            return report;
        }

        /// <summary>One row per invoice, which is the register a book keeper reads.</summary>
        private async Task<SalesReportDto> BuildSalesInvoiceAsync(SalesReportFilterDto filter, DateTime from,
            DateTime to)
        {
            var invoices = FilterInvoices(await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByDateRangeAsync(from, to), filter, includeAllStatuses: true);

            var report = Report(filter, "Sales Invoice Register",
                "One row per invoice raised in the period, whatever state it is in.",
                new SalesReportColumnDto("Date"),
                new SalesReportColumnDto("Invoice No"),
                new SalesReportColumnDto("Customer"),
                new SalesReportColumnDto("Salesperson"),
                new SalesReportColumnDto("Status"),
                new SalesReportColumnDto("Sub Total", true),
                new SalesReportColumnDto("Discount", true),
                new SalesReportColumnDto("Tax", true),
                new SalesReportColumnDto("Grand Total", true),
                new SalesReportColumnDto("Due", true));

            decimal subTotal = 0, discount = 0, tax = 0, grandTotal = 0, due = 0;

            foreach (var invoice in invoices.OrderBy(x => x.InvoiceDate))
            {
                subTotal += invoice.SubTotal;
                discount += invoice.ItemDiscountTotal;
                tax += invoice.TotalTax;
                grandTotal += invoice.GrandTotal;
                due += invoice.DueAmount;

                report.Rows.Add(new List<string>
                {
                    invoice.InvoiceDate.ToString("dd-MM-yyyy"),
                    invoice.InvoiceNo,
                    invoice.Customer?.CustomerName ?? string.Empty,
                    invoice.Salesperson?.SalespersonName ?? "—",
                    invoice.Status.ToString(),
                    invoice.SubTotal.ToString("N2"),
                    invoice.ItemDiscountTotal.ToString("N2"),
                    invoice.TotalTax.ToString("N2"),
                    invoice.GrandTotal.ToString("N2"),
                    invoice.DueAmount.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                string.Empty, string.Empty, string.Empty, string.Empty,
                $"{report.Rows.Count} invoices",
                subTotal.ToString("N2"),
                discount.ToString("N2"),
                tax.ToString("N2"),
                grandTotal.ToString("N2"),
                due.ToString("N2")
            };

            return report;
        }

        /// <summary>
        /// One row per salesperson: what they sold in the period and what that earned
        /// them, taken from the same invoices so the two can never disagree.
        /// </summary>
        private async Task<SalesReportDto> BuildSalespersonPerformanceAsync(SalesReportFilterDto filter,
            DateTime from, DateTime to)
        {
            var invoices = FilterInvoices(await _salesReportUnitOfWork
                .SalesInvoiceRepository
                .GetSalesInvoicesByDateRangeAsync(from, to), filter)
                .Where(x => x.SalespersonId.HasValue)
                .ToList();

            var commissions = (await _salesReportUnitOfWork
                .SalesCommissionRepository
                .GetSalesCommissionsByDateRangeAsync(from, to))
                .Where(x => x.Status != CommissionStatus.Cancelled)
                .Where(x => !filter.SalespersonId.HasValue || x.SalespersonId == filter.SalespersonId.Value)
                .ToList();

            var report = Report(filter, "Salesperson Performance",
                "One row per salesperson, taken from the invoices they closed in the period.",
                new SalesReportColumnDto("Salesperson"),
                new SalesReportColumnDto("Code"),
                new SalesReportColumnDto("Invoices", true),
                new SalesReportColumnDto("Customers", true),
                new SalesReportColumnDto("Sales Value", true),
                new SalesReportColumnDto("Target", true),
                new SalesReportColumnDto("Achieved %", true),
                new SalesReportColumnDto("Commission", true));

            // A prorated target, because half a month should not be measured against a
            // whole month's number.
            var months = MonthsIn(from, to);

            decimal value = 0, target = 0, commission = 0;
            var invoiceCount = 0;

            foreach (var group in invoices
                .GroupBy(x => new
                {
                    SalespersonId = x.SalespersonId!.Value,
                    Name = x.Salesperson?.SalespersonName ?? string.Empty,
                    Code = x.Salesperson?.SalespersonCode ?? string.Empty,
                    MonthlyTarget = x.Salesperson?.MonthlyTarget ?? 0m
                })
                .OrderByDescending(x => x.Sum(i => i.GrandTotal)))
            {
                var groupValue = group.Sum(x => x.GrandTotal);
                var groupTarget = Round(group.Key.MonthlyTarget * months);
                var groupCommission = commissions
                    .Where(x => x.SalespersonId == group.Key.SalespersonId)
                    .Sum(x => x.CommissionAmount);

                value += groupValue;
                target += groupTarget;
                commission += groupCommission;
                invoiceCount += group.Count();

                report.Rows.Add(new List<string>
                {
                    group.Key.Name,
                    group.Key.Code,
                    group.Count().ToString("N0"),
                    group.Select(x => x.CustomerId).Distinct().Count().ToString("N0"),
                    groupValue.ToString("N2"),
                    groupTarget > 0 ? groupTarget.ToString("N2") : "—",
                    groupTarget > 0 ? (groupValue / groupTarget * 100m).ToString("N1") : "—",
                    groupCommission.ToString("N2")
                });
            }

            report.Totals = new List<string>
            {
                $"{report.Rows.Count} salespeople",
                string.Empty,
                invoiceCount.ToString("N0"),
                invoices.Select(x => x.CustomerId).Distinct().Count().ToString("N0"),
                value.ToString("N2"),
                target > 0 ? target.ToString("N2") : "—",
                target > 0 ? (value / target * 100m).ToString("N1") : "—",
                commission.ToString("N2")
            };

            return report;
        }

        private static SalesReportDto Report(SalesReportFilterDto filter, string title, string description,
            params SalesReportColumnDto[] columns)
        {
            return new SalesReportDto
            {
                Title = title,
                Description = description,
                FromDate = filter.FromDate.Date,
                ToDate = filter.ToDate.Date,
                Columns = columns.ToList()
            };
        }

        private static List<SalesOrder> Filter(IEnumerable<SalesOrder> orders, SalesReportFilterDto filter)
        {
            return orders
                .Where(x => x.Status != SalesOrderStatus.Cancelled)
                .Where(x => !filter.CustomerId.HasValue || x.CustomerId == filter.CustomerId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.BusinessLocationId == filter.BusinessLocationId.Value)
                .Where(x => !filter.SalespersonId.HasValue || x.SalespersonId == filter.SalespersonId.Value)
                .Where(x => !filter.ProductId.HasValue
                    || (x.SalesOrderItems ?? new List<SalesOrderItem>())
                        .Any(line => line.ProductId == filter.ProductId.Value))
                .ToList();
        }

        /// <summary>
        /// A draft invoice has claimed nothing and a cancelled one has been taken back,
        /// so neither counts as sales. The register is the one report that shows both,
        /// because its job is to list every document raised.
        /// </summary>
        private static List<SalesInvoice> FilterInvoices(IEnumerable<SalesInvoice> invoices,
            SalesReportFilterDto filter, bool includeAllStatuses = false)
        {
            return invoices
                .Where(x => includeAllStatuses || CountableInvoiceStatuses.Contains(x.Status))
                .Where(x => !filter.CustomerId.HasValue || x.CustomerId == filter.CustomerId.Value)
                .Where(x => !filter.BusinessLocationId.HasValue
                    || x.BusinessLocationId == filter.BusinessLocationId.Value)
                .Where(x => !filter.SalespersonId.HasValue || x.SalespersonId == filter.SalespersonId.Value)
                .Where(x => !filter.ProductId.HasValue
                    || (x.SalesInvoiceItems ?? new List<SalesInvoiceItem>())
                        .Any(line => line.ProductId == filter.ProductId.Value))
                .ToList();
        }

        private static bool MatchesProduct(Guid productId, SalesReportFilterDto filter)
        {
            return !filter.ProductId.HasValue || productId == filter.ProductId.Value;
        }

        /// <summary>
        /// How many months long the period is, so a monthly target can be scaled to it.
        /// </summary>
        private static decimal MonthsIn(DateTime from, DateTime to)
        {
            var days = (to.Date - from.Date).TotalDays + 1;

            return Math.Max(0m, Math.Round((decimal)days / 30m, 4, MidpointRounding.AwayFromZero));
        }

        private static decimal Round(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}
