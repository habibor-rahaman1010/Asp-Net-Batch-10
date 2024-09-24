using DevSkill.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.ServicesContract
{
    public interface IUnitManagementService
    {
        Task<IList<Unit>> GetAllUnitAsync();
        Task<Unit> GetUnitByIdAsync(Guid id);
    }
}
