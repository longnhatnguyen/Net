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
    public class UserRateCyberRepository : GenericRepository<RatingCyber>, IUserRateCyberRepository
    {
        public async Task<PagingOutput<RatingCyber>> GetAllRateCyberByCyberId(int cyberId, int indexPage, int pageSize)
        {
            var rates = (await FindBy(x => x.CyberId == cyberId)).ToList();
            if (rates.Count > 0)
            {
                var lstRates = from r in rates
                               orderby r.UpdatedDate descending
                               select r;
                var data = lstRates.Skip((indexPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new RatingCyber()
                    {
                        Id = x.Id,
                        CommentContent = x.CommentContent,
                        CreatedDate = x.CreatedDate,
                        CyberId = x.CyberId,
                        Edited = x.Edited,
                        RatePoint = x.RatePoint,
                        UpdatedDate = x.UpdatedDate,
                        OrderId = x.OrderId,
                        UserId = x.UserId
                    }).ToList();
                int totalPage = rates.Count / pageSize;
                if (rates.Count % pageSize > 0)
                {
                    totalPage = (rates.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<RatingCyber>>
                {
                    Index = indexPage,
                    PageSize = pageSize,
                    TotalItem = rates.Count,
                    TotalPage = totalPage,
                    Data = data
                };
            }
            return null;
        }

        public async Task<IEnumerable<RatingCyber>> GetListRateCyberByCyberId(int cyberId)
        {
            return (await FindBy(x => x.CyberId == cyberId)).ToList();
        }

        public async Task<RatingCyber> GetRateCyberById(int id)
        {
            return (await FindBy(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<RatingCyber> GetRateCyberExist(int userId, int cyberId, int orderId)
        {
            return (await FindBy(x => x.UserId == userId && x.CyberId == cyberId && x.OrderId == orderId)).FirstOrDefault();
        }
    }
}
