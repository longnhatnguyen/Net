using CyberBook_API.Dal;
using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface IRoomTypeRepository : IGenericRepository<RoomType>
    {
        Task<IEnumerable<RoomType>> GetRoomTypeByRoomId(int? cyberId);
        Task<RoomType> GetRoomTypeById(int? id);
    }
}
