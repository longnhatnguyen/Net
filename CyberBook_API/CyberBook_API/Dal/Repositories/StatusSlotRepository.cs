using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberBook_API.Dal.Repositories
{
    public class StatusSlotRepository : GenericRepository<StatusSlot>, IStatusSlotRepository
    {
      
        public async Task<StatusSlot> GetStatusSlotsById(int? statusSlotId)
        {
            return (await FindBy(x => x.Id == statusSlotId)).FirstOrDefault();
                                 
        }
    }
}
