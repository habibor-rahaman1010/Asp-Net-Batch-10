using DevSkill.Inventory.Application.ServicesContract;
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

        public async Task<IList<Subcategory>> GetAllSubcategoryAsync()
        {
            return await _subCategoryUnitOfWork.SubCategoryRepository.GetAllAsync();
        }

        public async Task<Subcategory> GetSubcategoryByIdAsync(Guid id)
        {
            return await _subCategoryUnitOfWork.SubCategoryRepository.GetByIdAsync(id);
        }
    }
}
