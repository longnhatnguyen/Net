using CyberBook_API.Dal;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface IHardwareConfigRepository : IGenericRepository<SlotHardwareConfig>
    {
        Task<PagingOutput<IEnumerable<SlotHardwareConfig>>> GetSlotHardwareByCyberIdPaging(int cyberId, int pageIndex, int pageSize);
        Task<SlotHardwareConfig> GetSlotHardwareByNameAndCyber(string hardwareName, int cyberId);
        Task<SlotHardwareConfig> GetSlotHardwareById(int hardwareId);
        Task<IEnumerable<SlotHardwareConfig>> GetSlotHardwareByCyberId(int cyberId);

    }
}
