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
    public class SellingPriceTaxManagementService : ISellingPriceTaxManagementService
    {
        private readonly IInventoryUnitOfWork _sellingPriceUnitOfWork;
        public SellingPriceTaxManagementService(IInventoryUnitOfWork sellingPriceUnitOfWork)
        {
            _sellingPriceUnitOfWork = sellingPriceUnitOfWork;
        }

        public async Task AddSellingPriceTaxAsync(SellingPriceTax sellingPriceTax)
        {
            await _sellingPriceUnitOfWork.SellingPriceTaxRepository.AddAsync(sellingPriceTax);
            await _sellingPriceUnitOfWork.SaveAsync();
        }

        public async Task DeleteSellingPriceTaxAsync(Guid id)
        {
            await _sellingPriceUnitOfWork.SellingPriceTaxRepository.RemoveAsync(id);
            await _sellingPriceUnitOfWork.SaveAsync();
        }

        public async Task<IList<SellingPriceTax>> GetAllSellingPriceTax()
        {
            return await _sellingPriceUnitOfWork.SellingPriceTaxRepository.GetAllAsync();
        }

        public async Task<(IList<SellingPriceTax> data, int total, int totalDisplay)> GetAllSellingPriceTaxAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _sellingPriceUnitOfWork.SellingPriceTaxRepository.GetPagedSellingPriceTaxesAsync(pageIndex, pageSize, search, order);
        }

        public async Task<SellingPriceTax> GetSellingPriceTaxByIdAsync(Guid id)
        {
            return await _sellingPriceUnitOfWork.SellingPriceTaxRepository.GetByIdAsync(id);
        }

        public async Task UpdateSellingPriceTaxAsync(SellingPriceTax sellingPriceTax)
        {
            await _sellingPriceUnitOfWork.SellingPriceTaxRepository.EditAsync(sellingPriceTax);
            await _sellingPriceUnitOfWork.SaveAsync();
        }
    }
}
