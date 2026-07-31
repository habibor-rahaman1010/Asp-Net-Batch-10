using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;

namespace DevSkill.Inventory.Application.Services
{
    public class CategoryManagementService : ICategoryManagementService
    {
        private readonly IInventoryUnitOfWork _categoryUnitOfWork;
        public CategoryManagementService(IInventoryUnitOfWork categoryUnitOfWork)
        {
            _categoryUnitOfWork = categoryUnitOfWork;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryUnitOfWork.CategoryRepository.AddAsync(category);
            await _categoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryUnitOfWork.CategoryRepository.RemoveAsync(id);
            await _categoryUnitOfWork.SaveAsync();
        }

        public async Task<IList<Category>> GetCategoriesAsync()
        {
            return await _categoryUnitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<(IList<Category> data, int total, int totalDisplay)> GetCategoriesAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _categoryUnitOfWork.CategoryRepository.GetPagedCategoriesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _categoryUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task<int> GetTotalCategoryCount()
        {
            try
            {
                return await _categoryUnitOfWork.CategoryRepository.GetCountAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryUnitOfWork.CategoryRepository.EditAsync(category);
            await _categoryUnitOfWork.SaveAsync();
        }
    }
}
