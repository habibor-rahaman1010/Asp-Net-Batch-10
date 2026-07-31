using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;


namespace DevSkill.Inventory.Application.Services
{
    public class BrandManagementService : IBrandManagementService
    {
        private readonly IInventoryUnitOfWork _brandUnitOfWork;
        public BrandManagementService(IInventoryUnitOfWork brandUnitOfWork)
        {
            _brandUnitOfWork = brandUnitOfWork;
        }

        public async Task AddBrandAsync(Brand brand)
        {
            await _brandUnitOfWork.BrandRepository.AddAsync(brand);
            await _brandUnitOfWork.SaveAsync();

        }

        public async Task DeleteBrandAsync(Guid id)
        {
            await _brandUnitOfWork.BrandRepository.RemoveAsync(id);
            await _brandUnitOfWork.SaveAsync();
        }

        public async Task<IList<Brand>> GetAllBrandAsync()
        {
            return await _brandUnitOfWork.BrandRepository.GetAllAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(Guid id)
        {
            return await _brandUnitOfWork.BrandRepository.GetByIdAsync(id);
        }

        public async Task<(IList<Brand> data, int total, int totalDisplay)> GetBrandsAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order)
        {
            return await _brandUnitOfWork.BrandRepository.GetPagedBrandsAsync(pageIndex, pageSize, search, order);
        }

        public async Task<int> GetTotalBrandCount()
        {
            try
            {
                return await _brandUnitOfWork.BrandRepository.GetCountAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            await _brandUnitOfWork.BrandRepository.EditAsync(brand);
            await _brandUnitOfWork.SaveAsync();
        }
    }
}
