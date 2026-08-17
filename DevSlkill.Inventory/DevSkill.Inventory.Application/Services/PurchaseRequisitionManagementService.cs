using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Entities.PurchaseEntities;
using DevSkill.Inventory.Domain.Enums;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class PurchaseRequisitionManagementService : IPurchaseRequisitionManagementService
    {
        private readonly IInventoryUnitOfWork _purchaseRequisitionUnitOfWork;

        public PurchaseRequisitionManagementService(IInventoryUnitOfWork unitOfWork)
        {
            _purchaseRequisitionUnitOfWork = unitOfWork;
        }

        public async Task<Guid> CreatePurchaseRequisitionAsync(PurchaseRequisition purchaseRequisition)
        {
            await _purchaseRequisitionUnitOfWork.BeginTransactionAsync();

            try
            {
                var context = await ValidateAsync(purchaseRequisition);
                var requisitionId = Guid.NewGuid();

                var lines = BuildLines(context.RequestedItems, requisitionId);

                var header = new PurchaseRequisition
                {
                    Id = requisitionId,
                    RequisitionNo = await GenerateRequisitionNoAsync(),
                    RequisitionDate = context.RequisitionDate,
                    RequiredDate = purchaseRequisition.RequiredDate,
                    BusinessLocationId = context.Warehouse.Id,
                    RequestedById = purchaseRequisition.RequestedById,
                    RequestedByName = purchaseRequisition.RequestedByName ?? string.Empty,
                    Notes = purchaseRequisition.Notes ?? string.Empty,
                    Status = RestrictCreateStatus(purchaseRequisition.Status),
                    EstimatedTotal = SumEstimate(lines),
                    CreatedBy = purchaseRequisition.CreatedBy ?? string.Empty,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    PurchaseRequisitionItems = lines
                };

                await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.AddAsync(header);
                await _purchaseRequisitionUnitOfWork.CommitTransactionAsync();

                return requisitionId;
            }
            catch (InvalidOperationException)
            {
                await _purchaseRequisitionUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseRequisitionUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> UpdatePurchaseRequisitionAsync(PurchaseRequisition purchaseRequisition)
        {
            await _purchaseRequisitionUnitOfWork.BeginTransactionAsync();

            try
            {
                var existing = await _purchaseRequisitionUnitOfWork
                    .PurchaseRequisitionRepository
                    .GetPurchaseRequisitionByIdAsync(purchaseRequisition.Id)
                    ?? throw new InvalidOperationException("Purchase requisition not found.");

                GuardAgainstLockedStatus(existing.Status, "edited");

                // The warehouse decides which products the lines may hold, so it stays
                // as it was raised.
                purchaseRequisition.BusinessLocationId = existing.BusinessLocationId;

                var context = await ValidateAsync(purchaseRequisition);

                // The old lines go away and the new set is rebuilt, so a stale line can
                // never survive an edit.
                await _purchaseRequisitionUnitOfWork
                    .PurchaseRequisitionItemRepository
                    .RemoveRangeAsync(existing.PurchaseRequisitionItems?.ToList() ?? new List<PurchaseRequisitionItem>());

                var lines = BuildLines(context.RequestedItems, existing.Id);

                foreach (var line in lines)
                {
                    await _purchaseRequisitionUnitOfWork.PurchaseRequisitionItemRepository.AddAsync(line);
                }

                // The requisition number is issued once and never changes.
                existing.RequisitionDate = context.RequisitionDate;
                existing.RequiredDate = purchaseRequisition.RequiredDate;
                existing.RequestedById = purchaseRequisition.RequestedById;
                existing.RequestedByName = purchaseRequisition.RequestedByName ?? string.Empty;
                existing.Notes = purchaseRequisition.Notes ?? string.Empty;
                existing.Status = RestrictUpdateStatus(purchaseRequisition.Status);
                existing.EstimatedTotal = SumEstimate(lines);
                existing.Updated = DateTime.Now;

                await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.EditAsync(existing);
                await _purchaseRequisitionUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseRequisitionUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseRequisitionUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<bool> DeletePurchaseRequisitionAsync(Guid id)
        {
            await _purchaseRequisitionUnitOfWork.BeginTransactionAsync();

            try
            {
                var requisition = await _purchaseRequisitionUnitOfWork
                    .PurchaseRequisitionRepository
                    .GetPurchaseRequisitionByIdAsync(id)
                    ?? throw new InvalidOperationException("Purchase requisition not found.");

                GuardAgainstLockedStatus(requisition.Status, "deleted");

                await _purchaseRequisitionUnitOfWork
                    .PurchaseRequisitionItemRepository
                    .RemoveRangeAsync(requisition.PurchaseRequisitionItems?.ToList() ?? new List<PurchaseRequisitionItem>());

                await _purchaseRequisitionUnitOfWork
                    .PurchaseRequisitionRepository
                    .RemoveAsync(requisition);

                await _purchaseRequisitionUnitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (InvalidOperationException)
            {
                await _purchaseRequisitionUnitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception ex)
            {
                await _purchaseRequisitionUnitOfWork.RollbackTransactionAsync();
                throw new ApplicationException("Exception Occured: ", ex);
            }
        }

        public async Task<PurchaseRequisition?> GetPurchaseRequisitionByIdAsync(Guid id)
        {
            return await _purchaseRequisitionUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionByIdAsync(id);
        }

        public async Task<(IList<PurchaseRequisition> data, int total, int totalDisplay)> GetPurchaseRequisitionsAsync(
            int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _purchaseRequisitionUnitOfWork
                .PurchaseRequisitionRepository
                .GetPagedPurchaseRequisitionsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<IList<PurchaseRequisition>> GetPurchaseRequisitionsByStatusAsync(
            params PurchaseRequisitionStatus[] statuses)
        {
            return await _purchaseRequisitionUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionsByStatusAsync(statuses);
        }

        public async Task<string> GenerateRequisitionNoAsync()
        {
            var count = await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.GetCountAsync();

            string requisitionNo;
            do
            {
                count++;
                requisitionNo = $"PR-{DateTime.Now:yyyyMM}-{count:D4}";
            }
            while (await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.IsRequisitionNoDuplicateAsync(requisitionNo));

            return requisitionNo;
        }

        /// <summary>
        /// Hands the requisition to the approver. Only a draft can be sent, and it
        /// must carry at least one line.
        /// </summary>
        public async Task SubmitPurchaseRequisitionAsync(Guid id)
        {
            var requisition = await _purchaseRequisitionUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionByIdAsync(id)
                ?? throw new InvalidOperationException("Purchase requisition not found.");

            if (requisition.Status != PurchaseRequisitionStatus.Draft)
            {
                throw new InvalidOperationException("Only a draft requisition can be submitted for approval.");
            }

            if (requisition.PurchaseRequisitionItems == null || requisition.PurchaseRequisitionItems.Count == 0)
            {
                throw new InvalidOperationException("A requisition without any product line cannot be submitted.");
            }

            requisition.Status = PurchaseRequisitionStatus.Submitted;
            requisition.Updated = DateTime.Now;

            await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.EditAsync(requisition);
            await _purchaseRequisitionUnitOfWork.SaveAsync();
        }

        public async Task ApprovePurchaseRequisitionAsync(Guid id, Guid? approvedById, string approvedByName)
        {
            var requisition = await GetSubmittedRequisitionAsync(id, "approved");

            requisition.Status = PurchaseRequisitionStatus.Approved;
            requisition.ApprovedById = approvedById;
            requisition.ApprovedByName = approvedByName ?? string.Empty;
            requisition.ApprovedDate = DateTime.Now;
            requisition.RejectionReason = string.Empty;
            requisition.Updated = DateTime.Now;

            await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.EditAsync(requisition);
            await _purchaseRequisitionUnitOfWork.SaveAsync();
        }

        public async Task RejectPurchaseRequisitionAsync(Guid id, Guid? approvedById, string approvedByName, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new InvalidOperationException("A rejection reason is required.");
            }

            var requisition = await GetSubmittedRequisitionAsync(id, "rejected");

            requisition.Status = PurchaseRequisitionStatus.Rejected;
            requisition.ApprovedById = approvedById;
            requisition.ApprovedByName = approvedByName ?? string.Empty;
            requisition.ApprovedDate = DateTime.Now;
            requisition.RejectionReason = reason.Trim();
            requisition.Updated = DateTime.Now;

            await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.EditAsync(requisition);
            await _purchaseRequisitionUnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Closes a requisition that is no longer wanted. Anything already turned into
        /// a purchase order is out of reach, so those cannot be cancelled here.
        /// </summary>
        public async Task CancelPurchaseRequisitionAsync(Guid id)
        {
            var requisition = await _purchaseRequisitionUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionByIdAsync(id)
                ?? throw new InvalidOperationException("Purchase requisition not found.");

            GuardAgainstLockedStatus(requisition.Status, "cancelled");

            requisition.Status = PurchaseRequisitionStatus.Cancelled;
            requisition.Updated = DateTime.Now;

            await _purchaseRequisitionUnitOfWork.PurchaseRequisitionRepository.EditAsync(requisition);
            await _purchaseRequisitionUnitOfWork.SaveAsync();
        }

        private async Task<PurchaseRequisition> GetSubmittedRequisitionAsync(Guid id, string action)
        {
            var requisition = await _purchaseRequisitionUnitOfWork
                .PurchaseRequisitionRepository
                .GetPurchaseRequisitionByIdAsync(id)
                ?? throw new InvalidOperationException("Purchase requisition not found.");

            if (requisition.Status != PurchaseRequisitionStatus.Submitted)
            {
                throw new InvalidOperationException(
                    $"Only a submitted requisition can be {action}.");
            }

            return requisition;
        }

        /// <summary>
        /// Everything create and update both need before any line can be built.
        /// </summary>
        private async Task<RequisitionContext> ValidateAsync(PurchaseRequisition purchaseRequisition)
        {
            var warehouse = await _purchaseRequisitionUnitOfWork
                .BusinessLocationRepository
                .GetByIdAsync(purchaseRequisition.BusinessLocationId)
                ?? throw new InvalidOperationException("Warehouse not found.");

            if (!warehouse.IsActive)
            {
                throw new InvalidOperationException(
                    $"'{warehouse.LocationName}' is inactive, so a requisition cannot be raised for it.");
            }

            var requisitionDate = purchaseRequisition.RequisitionDate == default
                ? DateTime.Now
                : purchaseRequisition.RequisitionDate;

            if (purchaseRequisition.RequiredDate.Date < requisitionDate.Date)
            {
                throw new InvalidOperationException("Required Date cannot be earlier than the requisition date.");
            }

            var grouped = purchaseRequisition.PurchaseRequisitionItems?
                .Where(x => x.ProductId != Guid.Empty && x.Quantity > 0)
                .GroupBy(x => x.ProductId)
                .Select(g => new RequestedLine(
                    g.Key,
                    g.Sum(x => x.Quantity),
                    g.Max(x => x.EstimatedUnitPrice),
                    g.Select(x => x.Remarks).FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty))
                .ToList();

            if (grouped == null || grouped.Count == 0)
            {
                throw new InvalidOperationException("At least one product line with a positive quantity is required.");
            }

            var requestedItems = new List<ValidatedLine>();

            foreach (var requested in grouped)
            {
                var product = await _purchaseRequisitionUnitOfWork
                    .ProductRepository
                    .GetByIdAsync(requested.ProductId)
                    ?? throw new InvalidOperationException("Product not found.");

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException($"'{product.ProductName}' is inactive and cannot be requisitioned.");
                }

                GuardWarehouse(product, warehouse);

                if (requested.EstimatedUnitPrice < 0)
                {
                    throw new InvalidOperationException(
                        $"The estimated price of '{product.ProductName}' cannot be negative.");
                }

                requestedItems.Add(new ValidatedLine(
                    product.Id, requested.Quantity, requested.EstimatedUnitPrice, requested.Remarks));
            }

            return new RequisitionContext(warehouse, requestedItems, requisitionDate);
        }

        /// <summary>
        /// The dropdown only offers the warehouse's own products; this makes the same
        /// rule hold for anything that reaches the server another way.
        /// </summary>
        private static void GuardWarehouse(Product product, BusinessLocation warehouse)
        {
            if (product.BusinessLocationId != warehouse.Id)
            {
                throw new InvalidOperationException(
                    $"'{product.ProductName}' does not belong to '{warehouse.LocationName}'.");
            }
        }

        private static List<PurchaseRequisitionItem> BuildLines(IList<ValidatedLine> lines, Guid requisitionId)
        {
            return lines.Select(x => new PurchaseRequisitionItem
            {
                Id = Guid.NewGuid(),
                PurchaseRequisitionId = requisitionId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                EstimatedUnitPrice = x.EstimatedUnitPrice,
                OrderedQuantity = 0,
                Remarks = x.Remarks
            }).ToList();
        }

        private static decimal SumEstimate(IEnumerable<PurchaseRequisitionItem> lines)
        {
            return Math.Round(lines.Sum(x => x.Quantity * x.EstimatedUnitPrice), 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// A requisition that is being approved, has already produced purchase orders
        /// or was closed is out of the requester's hands.
        /// </summary>
        private static void GuardAgainstLockedStatus(PurchaseRequisitionStatus status, string action)
        {
            if (status == PurchaseRequisitionStatus.Submitted)
            {
                throw new InvalidOperationException(
                    $"A requisition waiting for approval cannot be {action}. Ask the approver to reject it first.");
            }

            if (status == PurchaseRequisitionStatus.PartiallyConverted || status == PurchaseRequisitionStatus.Converted)
            {
                throw new InvalidOperationException($"A requisition that already has a purchase order cannot be {action}.");
            }

            if (status == PurchaseRequisitionStatus.Cancelled)
            {
                throw new InvalidOperationException($"A cancelled requisition cannot be {action}.");
            }
        }

        private static PurchaseRequisitionStatus RestrictCreateStatus(PurchaseRequisitionStatus status)
        {
            // A brand new requisition is either kept as a draft or sent straight for
            // approval. It can never approve itself.
            return status == PurchaseRequisitionStatus.Submitted
                ? PurchaseRequisitionStatus.Submitted
                : PurchaseRequisitionStatus.Draft;
        }

        private static PurchaseRequisitionStatus RestrictUpdateStatus(PurchaseRequisitionStatus status)
        {
            // Approval, rejection and conversion all belong to their own flows. An
            // edit may never reach for any of them.
            return status is PurchaseRequisitionStatus.Draft or PurchaseRequisitionStatus.Submitted
                ? status
                : PurchaseRequisitionStatus.Draft;
        }

        private sealed record RequestedLine(Guid ProductId, decimal Quantity, decimal EstimatedUnitPrice, string Remarks);

        private sealed record ValidatedLine(Guid ProductId, decimal Quantity, decimal EstimatedUnitPrice, string Remarks);

        private sealed record RequisitionContext(
            BusinessLocation Warehouse,
            IList<ValidatedLine> RequestedItems,
            DateTime RequisitionDate);
    }
}
