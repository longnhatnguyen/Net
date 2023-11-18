using CyberBook_API.Dal;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using CyberBook_API.ViewModel.SlotViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface ISlotsRepository : IGenericRepository<Slot>
    {
        Task<PagingOutput<IEnumerable<Slot>>> GetAllSlotByRoomId(int roomId, int pageIndex, int pageSize);
        Task<PagingOutput<IEnumerable<Slot>>> GetSlotByFilter(SlotsModelFilter modelFilter, int pageIndex, int pageSize);
        Task<Slot> GetSlotById(int slotId);
        Task<Slot> IsSlotReady(int slotId);
        
    }
}
