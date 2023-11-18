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
    public class RoomTypeRepository : GenericRepository<RoomType>, IRoomTypeRepository
    {
        public async Task<RoomType> GetRoomTypeById(int? id)
        {
            return (await FindBy(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<RoomType>> GetRoomTypeByRoomId(int? cyberId)
        {
            return (await FindBy(x => x.CyberID == cyberId)).ToList();
        }
    }
}
