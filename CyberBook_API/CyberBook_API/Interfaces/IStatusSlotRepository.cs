using CyberBook_API.Dal;
using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface IStatusSlotRepository : IGenericRepository<StatusSlot>
    {
        Task<StatusSlot> GetStatusSlotsById(int? statusSlotId);
    }
}
