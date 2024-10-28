using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IBarcodeTypeManagementService
    {
        Task<IList<BarcodeType>> GetBarCodeTypes();
        Task<BarcodeType> GetBarcodeTypeId(Guid id);
        Task<(IList<BarcodeType> data, int total, int totalDisplay)> GetAllBarcodeTypeAsync(int pageIndex, int pageSize, DataTablesSearch search, string? order);
        Task AddBarcodeTypeAsync(BarcodeType barcodeType);
        Task UpdateBarcodeTypeAsync(BarcodeType barcodeType);
        Task DeleteBarcodeTypeAsync(Guid id);
    }
}
