namespace DevSkill.Inventory.Domain.Enums
{
    /// <summary>
    /// Draft -> Submitted -> Approved/Rejected. Only an approved requisition can be
    /// turned into a purchase order, and Converted is written by that flow alone.
    /// </summary>
    public enum PurchaseRequisitionStatus
    {
        Draft = 0,
        Submitted = 1,
        Approved = 2,
        Rejected = 3,
        PartiallyConverted = 4,
        Converted = 5,
        Cancelled = 6
    }
}
