using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.RepositoryContracts
{
    public interface ISubCategoryRepository : IRepositoryBase<SubCategory, Guid>
    {
        Task<(IList<SubCategory> data, int total, int totalDisplay)> GetPagedSubCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
    }
}
