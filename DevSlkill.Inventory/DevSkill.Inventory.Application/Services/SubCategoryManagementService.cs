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
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public SubCategoryManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<IList<Subcategory>> GetAllSubcategoryAsync()
        {
            return await _inventoryUnitOfWork.SubCategoryRepository.GetAllAsync();
        }
    }
}
