using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface ICategoryManagementService
    {
        Task<IList<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryById(Guid id);
        Task<(IList<Category> data, int total, int totalDisplay)> GetCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(Category category);
        public Task<int> GetTotalCategoryCount();
    }
}
