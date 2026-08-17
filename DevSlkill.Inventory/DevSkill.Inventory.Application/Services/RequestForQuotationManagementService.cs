using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class RequestForQuotationManagementService : IRequestForQuotationManagementService
    {
        private readonly IInventoryUnitOfWork _requestForQuotationUnitOfWork;

        public RequestForQuotationManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _requestForQuotationUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateRequestForQuotationAsync(RequestForQuotation requestForQuotation)
        {
            await _requestForQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(requestForQuotation);
                var requestForQuotationId = Guid.NewGuid();

                var lines = BuildLines(context.Lines, requestForQuotationId);

                var header = new RequestForQuotation
                {
                    Id = requestForQuotationId,
                    RfqNo = await GenerateRfqNoAsync(),
                    PurchaseRequisitionId = context.PurchaseRequisitionId,
                    BusinessLocationId = requestForQuotation.BusinessLocationId,
                    RfqDate = context.RfqDate,
                    ResponseDeadline = context.ResponseDeadline,
                    Notes = requestForQuotation.Notes ?? string.Empty,
                    Status = requestForQuotation.Status == RequestForQuotationStatus.Sent
                        ? RequestForQuotationStatus.Sent
                        : RequestForQuotationStatus.Draft,
                    CreatedBy = requestForQuotation.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    RequestForQuotationItems = lines
                };

                await _requestForQuotationUnitOfWork.RequestForQuotationRepository.AddAsync(header);
                await _requestForQuotationUnitOfWork.CommitTransactionAsync();

                return requestForQuotationId;
            }
            catch (InvalidOperationException)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdateRequestForQuotationAsync(RequestForQuotation requestForQuotation)
        {
            await _requestForQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _requestForQuotationUnitOfWork
                    .RequestForQuotationRepository
                    .GetRequestForQuotationByIdAsync(requestForQuotation.Id)
                    ?? throw new InvalidOperationException("Request for quotation not found.");

                // Once it has gone out, the suppliers are quoting against these very
                // lines, so changing them would make the quotations incomparable.
                if (existing.Status != RequestForQuotationStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"A {existing.Status.ToString().ToLower()} request for quotation cannot be edited.");
                }

                var context = await ValidateAsync(requestForQuotation);

                await _requestForQuotationUnitOfWork
                    .RequestForQuotationItemRepository
                    .RemoveRangeAsync(existing.RequestForQuotationItems?.ToList()
                        ?? new List<RequestForQuotationItem>());

                var lines = BuildLines(context.Lines, existing.Id);

                foreach (var line in lines)
                {
                    await _requestForQuotationUnitOfWork.RequestForQuotationItemRepository.AddAsync(line);
                }

                // The request number is issued once and never changes.
                existing.PurchaseRequisitionId = context.PurchaseRequisitionId;
                existing.BusinessLocationId = requestForQuotation.BusinessLocationId;
                existing.RfqDate = context.RfqDate;
                existing.ResponseDeadline = context.ResponseDeadline;
                existing.Notes = requestForQuotation.Notes ?? string.Empty;
                existing.Updated = DateTime.Now;

                if (requestForQuotation.Status == RequestForQuotationStatus.Sent)
                {
                    existing.Status = RequestForQuotationStatus.Sent;
                }

                await _requestForQuotationUnitOfWork.RequestForQuotationRepository.EditAsync(existing);
                await _requestForQuotationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeleteRequestForQuotationAsync(Guid id)
        {
            await _requestForQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var requestForQuotation = await _requestForQuotationUnitOfWork
                    .RequestForQuotationRepository
                    .GetRequestForQuotationByIdAsync(id)
                    ?? throw new InvalidOperationException("Request for quotation not found.");

                await GuardAgainstQuotationsAsync(requestForQuotation.Id,
                    "This request already has supplier quotations against it and cannot be deleted. " +
                    "Cancel it instead, which keeps the record.");

                if (requestForQuotation.Status is RequestForQuotationStatus.Awarded)
                {
                    throw new InvalidOperationException(
                        "An awarded request for quotation cannot be deleted.");
                }

                await _requestForQuotationUnitOfWork
                    .RequestForQuotationItemRepository
                    .RemoveRangeAsync(requestForQuotation.RequestForQuotationItems?.ToList()
                        ?? new List<RequestForQuotationItem>());

                await _requestForQuotationUnitOfWork.RequestForQuotationRepository.RemoveAsync(requestForQuotation);
                await _requestForQuotationUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<RequestForQuotation?> GetRequestForQuotationByIdAsync(Guid id)
        {
            return await _requestForQuotationUnitOfWork
                .RequestForQuotationRepository
                .GetRequestForQuotationByIdAsync(id);
        }

        public async Task<(IList<RequestForQuotation> data, int total, int totalDisplay)>
            GetRequestForQuotationsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _requestForQuotationUnitOfWork
                .RequestForQuotationRepository
                .GetPagedRequestForQuotationsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<RequestForQuotation>> GetRequestForQuotationsByStatusAsync(
            params RequestForQuotationStatus[] statuses)
        {
            return await _requestForQuotationUnitOfWork
                .RequestForQuotationRepository
                .GetRequestForQuotationsByStatusAsync(statuses.Length == 0
                    ? new[] { RequestForQuotationStatus.Sent }
                    : statuses);
        }

        public async Task<string> GenerateRfqNoAsync()
        {
            var count = await _requestForQuotationUnitOfWork.RequestForQuotationRepository.GetCountAsync();

            string rfqNo;
            do
            {
                count++;
                rfqNo = $"RFQ-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _requestForQuotationUnitOfWork.RequestForQuotationRepository.IsRfqNoDuplicateAsync(rfqNo));

            return rfqNo;
        }

        public async Task SendRequestForQuotationAsync(Guid id)
        {
            await SetStatusAsync(id, RequestForQuotationStatus.Sent, current =>
            {
                if (current != RequestForQuotationStatus.Draft)
                {
                    throw new InvalidOperationException(
                        $"Only a draft request can be sent. This one is already {current.ToString().ToLower()}.");
                }
            });
        }

        public async Task CloseRequestForQuotationAsync(Guid id)
        {
            await SetStatusAsync(id, RequestForQuotationStatus.Closed, current =>
            {
                if (current != RequestForQuotationStatus.Sent)
                {
                    throw new InvalidOperationException(
                        $"Only a sent request can be closed. This one is {current.ToString().ToLower()}.");
                }
            });
        }

        public async Task CancelRequestForQuotationAsync(Guid id)
        {
            await SetStatusAsync(id, RequestForQuotationStatus.Cancelled, current =>
            {
                if (current == RequestForQuotationStatus.Cancelled)
                {
                    throw new InvalidOperationException("This request for quotation is already cancelled.");
                }

                // Undoing an award behind the back of a purchase order raised from the
                // winning quotation would break the chain.
                if (current == RequestForQuotationStatus.Awarded)
                {
                    throw new InvalidOperationException(
                        "An awarded request for quotation cannot be cancelled.");
                }
            });
        }

        private async Task SetStatusAsync(Guid id, RequestForQuotationStatus status,
            Action<RequestForQuotationStatus> guard)
        {
            await _requestForQuotationUnitOfWork.BeginTransactionAsync();

            try
            {
                var requestForQuotation = await _requestForQuotationUnitOfWork
                    .RequestForQuotationRepository
                    .GetRequestForQuotationByIdAsync(id)
                    ?? throw new InvalidOperationException("Request for quotation not found.");

                guard(requestForQuotation.Status);

                if (status == RequestForQuotationStatus.Sent
                    && (requestForQuotation.RequestForQuotationItems == null
                        || requestForQuotation.RequestForQuotationItems.Count == 0))
                {
                    throw new InvalidOperationException(
                        "A request with no lines has nothing for a supplier to quote for.");
                }

                requestForQuotation.Status = status;
                requestForQuotation.Updated = DateTime.Now;

                await _requestForQuotationUnitOfWork.RequestForQuotationRepository.EditAsync(requestForQuotation);
                await _requestForQuotationUnitOfWork.CommitTransactionAsync();
            }
            catch (InvalidOperationException)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _requestForQuotationUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        /// <summary>
        /// Everything create and update both need before any line can be built.
        /// </summary>
        private async Task<RequestContext> ValidateAsync(RequestForQuotation requestForQuotation)
        {
            if (requestForQuotation.BusinessLocationId == Guid.Empty)
            {
                throw new InvalidOperationException("A warehouse is required.");
            }

            _ = await _requestForQuotationUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(requestForQuotation.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            var rfqDate = requestForQuotation.RfqDate == default ? DateTime.Now : requestForQuotation.RfqDate;
            var deadline = requestForQuotation.ResponseDeadline == default
                ? rfqDate.AddDays(7)
                : requestForQuotation.ResponseDeadline;

            if (deadline.Date < rfqDate.Date)
            {
                throw new InvalidOperationException("The response deadline cannot be earlier than the request date.");
            }

            Guid? purchaseRequisitionId = null;

            if (requestForQuotation.PurchaseRequisitionId.HasValue
                && requestForQuotation.PurchaseRequisitionId.Value != Guid.Empty)
            {
                var requisition = await _requestForQuotationUnitOfWork
                    .PurchaseRequisitionRepository
                    .GetPurchaseRequisitionByIdAsync(requestForQuotation.PurchaseRequisitionId.Value)
                    ?? throw new InvalidOperationException("Purchase requisition not found.");

                // Quoting for something nobody has approved yet would put the buyer in
                // front of suppliers with a request that may never be bought.
                if (requisition.Status is not (PurchaseRequisitionStatus.Approved
                    or PurchaseRequisitionStatus.PartiallyConverted))
                {
                    throw new InvalidOperationException(
                        $"A {requisition.Status.ToString().ToLower()} requisition cannot be sent out for quotation. " +
                        "It has to be approved first.");
                }

                purchaseRequisitionId = requisition.Id;
            }

            var grouped = requestForQuotation.RequestForQuotationItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.Quantity),
                    g.Select(x => x.Remarks).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one product with a quantity is required.");
            }

            var validated = new List<ValidatedLine>();

            foreach (var requested in grouped)
            {
                var product = await _requestForQuotationUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                validated.Add(new ValidatedLine(product.Id, requested.Quantity, requested.Remarks));
            }

            return new RequestContext(purchaseRequisitionId, validated, rfqDate, deadline);
        }

        private async Task GuardAgainstQuotationsAsync(Guid requestForQuotationId, string message)
        {
            var quotations = await _requestForQuotationUnitOfWork
                .SupplierQuotationRepository
                .GetSupplierQuotationsByRequestAsync(requestForQuotationId);

            if (quotations.Any(x => x.Status != SupplierQuotationStatus.Cancelled))
            {
                throw new InvalidOperationException(message);
            }
        }

        private static List<RequestForQuotationItem> BuildLines(IList<ValidatedLine> lines,
            Guid requestForQuotationId)
        {
            return lines.Select(x => new RequestForQuotationItem
            {
                Id = Guid.NewGuid(),
                RequestForQuotationId = requestForQuotationId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Remarks = x.Remarks
            }).ToList();
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal Quantity, string Remarks);

        private sealed record RequestContext(
            Guid? PurchaseRequisitionId,
            IList<ValidatedLine> Lines,
            DateTime RfqDate,
            DateTime ResponseDeadline);
    }
}
