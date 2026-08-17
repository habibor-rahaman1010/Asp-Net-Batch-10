using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ICreditNoteItemRepository : IRepositoryBase<CreditNoteItem, Guid>
    {
        Task<IList<CreditNoteItem>> GetItemsByCreditNoteAsync(Guid creditNoteId);
        Task RemoveRangeAsync(IList<CreditNoteItem> items);
    }
}
