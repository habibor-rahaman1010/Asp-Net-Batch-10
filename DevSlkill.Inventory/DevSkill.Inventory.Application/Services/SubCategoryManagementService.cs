using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Services
{
    public class SubCategoryManagementService : ISubCategoryManagementService
    {
        private readonly IInventoryUnitOfWork _subCategoryUnitOfWork;
        public SubCategoryManagementService(IInventoryUnitOfWork subCategoryUnitOfWork)
        {
            _subCategoryUnitOfWork = subCategoryUnitOfWork;
        }

        public async Task AddSubCategoryAsync(SubCategory subCategory)
        {
            await _subCategoryUnitOfWork.SubCategoryRepository.AddAsync(subCategory);
            await _subCategoryUnitOfWork.SaveAsync();
        }

        public async Task DeleteSubCategoryAsync(Guid id)
        {
            await _subCategoryUnitOfWork.SubCategoryRepository.RemoveAsync(id);
            await _subCategoryUnitOfWork.SaveAsync();
        }

        public async Task<IList<SubCategory>> GetAllSubCategoryAsync()
        {
            return await _subCategoryUnitOfWork.SubCategoryRepository.GetAllAsync();
        }

        public async Task<(IList<SubCategory> data, int total, int totalDisplay)> GetAllSubCategoryAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _subCategoryUnitOfWork.SubCategoryRepository.GetPagedSubCategoriesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<SubCategory> GetSubCategoryByIdAsync(Guid id)
        {
            return await _subCategoryUnitOfWork.SubCategoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateSubCategoryAsync(SubCategory subCategory)
        {
            await _subCategoryUnitOfWork.SubCategoryRepository.EditAsync(subCategory);
            await _subCategoryUnitOfWork.SaveAsync();
        }
    }
}
