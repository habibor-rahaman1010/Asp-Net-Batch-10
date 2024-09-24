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
    public class SellingPriceTaxManagementService : ISellingPriceTaxManagementService
    {
        private readonly IInventoryUnitOfWork _inventoryUnitOfWork;
        public SellingPriceTaxManagementService(IInventoryUnitOfWork inventoryUnitOfWork)
        {
            _inventoryUnitOfWork = inventoryUnitOfWork;
        }

        public async Task<IList<SellingPriceTax>> GetAllSellingPriceTax()
        {
            return await _inventoryUnitOfWork.SellingPriceTaxRepository.GetAllAsync();
        }
    }
}
