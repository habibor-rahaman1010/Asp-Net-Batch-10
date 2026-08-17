using DevSkill.Inventory.Domain.Entities.SalesEntities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISalespersonRepository : IRepositoryBase<Salesperson, Guid>
    {
        Task<(IList<Salesperson> data, int total, int totalDisplay)> GetPagedSalespersonsAsync(int pageIndex,
            int pageSize, DataTablesSearch search, string? order);
        Task<Salesperson?> GetSalespersonByIdAsync(Guid id);
        Task<IList<Salesperson>> GetActiveSalespersonsAsync();
        Task<bool> IsSalespersonCodeDuplicateAsync(string salespersonCode, Guid? excludeId = null);
    }
}
