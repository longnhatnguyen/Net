using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using Microsoft.EntityFrameworkCore;

namespace CyberBook_API.Dal.Repositories
{
    public class HardwareConfigRepository : GenericRepository<SlotHardwareConfig>, IHardwareConfigRepository
    {
        public async Task<IEnumerable<SlotHardwareConfig>> GetSlotHardwareByCyberId(int cyberId)
        {
            return (await FindBy(x => x.CyberID == cyberId)).ToList();
        }

        public async Task<PagingOutput<IEnumerable<SlotHardwareConfig>>> GetSlotHardwareByCyberIdPaging(int cyberId, int pageIndex, int pageSize)
        {
            var hardwares = (await FindBy(x => x.CyberID == cyberId)).ToList();
            if (hardwares.Count > 0)
            {
                var data = hardwares.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new SlotHardwareConfig() { 
                        Id = x.Id,
                        Cpu = x.Cpu,
                        CreatedDate = x.CreatedDate,
                        CyberID = x.CyberID,
                        Gpu = x.Gpu,
                        Monitor =x.Monitor,
                        NameHardware = x.NameHardware,
                        Ram = x.Ram,
                        UpdateDate = x.UpdateDate
                    }).ToList();
                int totalPage = hardwares.Count / pageSize;
                if (hardwares.Count % pageSize > 0)
                {
                    totalPage = (hardwares.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<SlotHardwareConfig>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = hardwares.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<SlotHardwareConfig> GetSlotHardwareById(int hardwareId)
        {
            return (await FindBy(x => x.Id == hardwareId)).FirstOrDefault();
        }   

        public async Task<SlotHardwareConfig> GetSlotHardwareByNameAndCyber(string hardwareName, int cyberId)
        {
            if(!string.IsNullOrWhiteSpace(hardwareName))
            {
                return (await FindBy(x => x.NameHardware.Equals(hardwareName) && x.CyberID == cyberId)).FirstOrDefault();
            }
            return null;
        }
    }
}
