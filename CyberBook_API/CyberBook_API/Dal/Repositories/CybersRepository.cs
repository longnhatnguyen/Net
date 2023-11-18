using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Interfaces;
using CyberBook_API.Enum;
using CyberBook_API.ViewModel.PagingView;

namespace CyberBook_API.Dal.Repositories
{
    public class CybersRepository : GenericRepository<Cyber>, ICybersRepository
    {
        public async Task<PagingOutput<IEnumerable<Cyber>>> GetAllCyberAvaiable(int pageIndex, int pageSize)
        {
            var cybers = (await FindBy(x => x.status == Convert.ToInt32(CybersEnum.CybersStatus.Active))).ToList();
            if (cybers.Count > 0)
            {
                var data = cybers.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new Cyber()
                    {
                        Id = x.Id,
                        Address = x.Address,
                        BossCyberID = x.BossCyberID,
                        BossCyberName = x.BossCyberName,
                        CyberDescription = x.CyberDescription,
                        CyberName = x.CyberName,
                        image = x.image,
                        lat = x.lat,
                        lng = x.lng,
                        PhoneNumber = x.PhoneNumber,
                        RatingPoint = x.RatingPoint,
                        status = x.status
                    }).ToList();
                int totalPage = cybers.Count / pageSize;
                if (cybers.Count % pageSize > 0)
                {
                    totalPage = (cybers.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Cyber>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = cybers.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<Cyber> GetCyberById(int? cyberId)
        {
            return (await FindBy(x => x.Id == cyberId)).FirstOrDefault();
        }

        public async Task<PagingOutput<IEnumerable<Cyber>>> GetCyberByName(string cyberName, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrWhiteSpace(cyberName))
            {
               var cybers = (await FindBy(x => x.CyberName.Contains(cyberName))).ToList();
                if (cybers.Count > 0)
                {
                    var data = cybers.Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .Select(x => new Cyber()
                        {
                            Id = x.Id,
                            Address = x.Address,
                            BossCyberID = x.BossCyberID,
                            BossCyberName = x.BossCyberName,
                            CyberDescription = x.CyberDescription,
                            CyberName = x.CyberName,
                            image = x.image,
                            lat = x.lat,
                            lng = x.lng,
                            PhoneNumber = x.PhoneNumber,
                            RatingPoint = x.RatingPoint,
                            status = x.status
                        }).ToList();
                    int totalPage = cybers.Count / pageSize;
                    if (cybers.Count % pageSize > 0)
                    {
                        totalPage = (cybers.Count / pageSize) + 1;
                    }

                    var paging = new PagingOutput<IEnumerable<Cyber>>
                    {
                        Index = pageIndex,
                        PageSize = pageSize,
                        TotalItem = cybers.Count,
                        TotalPage = totalPage,
                        Data = data
                    };
                    return paging;
                }
            }
            return null;
        }

        public async Task<PagingOutput<IEnumerable<Cyber>>> GetCyberByRatePoint(int pageIndex, int pageSize)
        {
            var cybers = (await GetAll()).ToList();
            if (cybers.Count > 0)
            {
                var lstCybers = from c in cybers
                                orderby c.RatingPoint descending
                                select c;
                var data = lstCybers.Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize)
                       .Select(x => new Cyber()
                       {
                           Id = x.Id,
                           Address = x.Address,
                           BossCyberID = x.BossCyberID,
                           BossCyberName = x.BossCyberName,
                           CyberDescription = x.CyberDescription,
                           CyberName = x.CyberName,
                           image = x.image,
                           lat = x.lat,
                           lng = x.lng,
                           PhoneNumber = x.PhoneNumber,
                           RatingPoint = x.RatingPoint,
                           status = x.status
                       }).ToList();


                int totalPage = cybers.Count / pageSize;
                if (cybers.Count % pageSize > 0)
                {
                    totalPage = (cybers.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<Cyber>>
                {
                    Index = pageIndex,
                    PageSize = pageSize,
                    TotalItem = cybers.Count,
                    TotalPage = totalPage,
                    Data = data
                };
                return paging;
            }
            return null;
        }

        public async Task<Cyber> GetCyberByBossCyberId(int? userId)
        {
            return (await FindBy(x => x.BossCyberID == userId)).FirstOrDefault();
        }

        public async Task<bool> IsBossCyber(int userId, int cyberId)
        {
            var cyber = (await FindBy(x => x.Id == cyberId)).FirstOrDefault();
            if (cyber.BossCyberID == userId)
            {
                return true;
            }
            return false;
        }

    }
}
