using CyberBook_API.Dal;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<Room> GetRoomById(int id);
        Task<Room> EditRoomSize(int roomId, int maxX, int maxY);
        Task<Room> EditRoomSizeAllow(int roomId, int maxX, int maxY);
        Task<PagingOutput<IEnumerable<Room>>> GetListRoomByCyberId(int cyberId, int pageIndex, int pageSize);
    }
}
