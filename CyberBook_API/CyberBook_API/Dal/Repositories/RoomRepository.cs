using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.Enum;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using Microsoft.EntityFrameworkCore;

namespace CyberBook_API.Dal.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly ISlotsRepository _slotsRepository = new SlotsRepository();

        public async Task<Room> EditRoomSize(int roomId, int maxX, int maxY)
        {
            var room = (await FindBy(x => x.Id == roomId)).FirstOrDefault();
            if (room != null)
            {
                var lstSlot = (await _slotsRepository.FindBy(x => x.RoomId == roomId)).ToList();
                int crrMaxX = int.MinValue;
                int crrMaxY = int.MinValue;
                foreach (var x in lstSlot)
                {
                    if (x.SlotPositionX > crrMaxX)
                    {
                        crrMaxX = Convert.ToInt32(x.SlotPositionX);
                    }
                    if (x.SlotPositionY > crrMaxY)
                    {
                        crrMaxY = Convert.ToInt32(x.SlotPositionY);
                    }
                }
                if (maxX <= crrMaxX || maxY <= crrMaxY)
                {
                    room.MaxX = maxX;
                    room.MaxY = maxY;
                    var newRoom = await Update(room, room.Id);
                    if (newRoom != -1)
                    {
                        return room;
                    }
                    return null;
                }
            }
            return null;
        }

        public async Task<Room> EditRoomSizeAllow(int roomId, int maxX, int maxY)
        {
            var room = (await FindBy(x => x.Id == roomId)).FirstOrDefault();
            if (room != null)
            {
                var lstSlot = (await _slotsRepository.FindBy(x => x.RoomId == roomId)).ToList();
                foreach (var x in lstSlot)
                {
                    if (x.SlotPositionX > maxX || x.SlotPositionY > maxY)
                    {
                        x.StatusId = Convert.ToInt32(SlotsEnum.SlotStatus.Unload);
                        if((await _slotsRepository.Update(x, x.Id)) == -1)
                        {
                            return null;
                        }
                    }
                }
                return room;
            }
            return null;
        }

        public async Task<PagingOutput<IEnumerable<Room>>> GetListRoomByCyberId(int cyberId, int pageIndex, int pageSize)
        {
            var room = (await FindBy(x => x.CyberId == cyberId)).ToList();
            if (room.Count > 0)
            {
                var data = room.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new Room()
                    {
                        Id = x.Id,
                        CyberId = x.CyberId,
                        MaxX = x.MaxX,
                        MaxY = x.MaxY,
                        RoomName = x.RoomName,
                        RoomPosition = x.RoomPosition,
                        RoomType = x.RoomType
                    }).ToList();
                int totalPage = room.Count / pageSize;
                if (room.Count % pageSize > 0)
                {
                    totalPage = (room.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Room>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = room.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<Room> GetRoomById(int id)
        {
            return (await FindBy(x => x.Id == id)).FirstOrDefault();
        }

        public int FindMax(List<Slot> list)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int max = int.MinValue;
            foreach (Slot s in list)
            {
                if (s.SlotPositionX > max)
                {
                    max = (int)s.SlotPositionX;
                }
            }
            return max;
        }

       
    }
}
