using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.Enum;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using CyberBook_API.ViewModel.SlotViewModel;
using Microsoft.EntityFrameworkCore;

namespace CyberBook_API.Dal.Repositories
{
    public class SlotsRepository : GenericRepository<Slot>, ISlotsRepository
    {
        public async Task<PagingOutput<IEnumerable<Slot>>> GetSlotByFilter(SlotsModelFilter modelFilter, int pageIndex, int pageSize)
        {
            var slots = (await FindBy(x =>
            x.RoomId == modelFilter.RoomId
            && x.StatusId == modelFilter.StatusId
            && x.SlotHardwareConfigId == modelFilter.ConfigId)).ToList();
            if (slots.Count > 0)
            {
                var data = slots.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new Slot()
                    {
                       Id = x.Id,
                       SlotHardwareConfigId = x.SlotHardwareConfigId,
                       StatusId = x.StatusId,
                       RoomId = x.RoomId,
                       SlotPositionX = x.SlotPositionX,
                       SlotPositionY = x.SlotPositionY,
                       SlotDescription = x.SlotDescription,
                       SlotHardwareName = x.SlotHardwareName,
                       SlotName = x.SlotName
                    }).ToList();
                int totalPage = slots.Count / pageSize;
                if (slots.Count % pageSize > 0)
                {
                    totalPage = (slots.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Slot>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = slots.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<Slot> GetSlotById(int slotId)
        {
            return (await FindBy(x => x.Id == slotId)).FirstOrDefault();
        }

        public async Task<PagingOutput<IEnumerable<Slot>>> GetAllSlotByRoomId(int roomId, int pageIndex, int pageSize)
        {
            var slots = (await FindBy(x => x.RoomId == roomId)).ToList();
            if (slots.Count > 0)
            {
                var data = slots.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new Slot()
                    {
                        Id = x.Id,
                        RoomId = x.RoomId,
                        SlotPositionX = x.SlotPositionX,
                        SlotPositionY = x.SlotPositionY,
                        SlotDescription = x.SlotDescription,
                        SlotHardwareConfigId = x.SlotHardwareConfigId,
                        SlotHardwareName = x.SlotHardwareName,
                        SlotName = x.SlotName,
                        StatusId = x.StatusId
                    }).ToList();
                int totalPage = slots.Count / pageSize;
                if (slots.Count % pageSize > 0)
                {
                    totalPage = (slots.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Slot>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = slots.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<Slot> IsSlotReady(int slotId)
        {
            return (await FindBy(x => x.Id == slotId && x.StatusId == Convert.ToInt32(SlotsEnum.SlotStatus.Ready))).FirstOrDefault();
        }
    }
}
