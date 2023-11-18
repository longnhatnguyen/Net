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
    public class CyberRateUserRepository : GenericRepository<RatingUser>, ICyberRateUserRepository
    {
        public async Task<PagingOutput<IEnumerable<RatingUser>>> GetRateUserByCyberId(int cyberId, int indexPage, int pageSize)
        {
            var rates = (await FindBy(x => x.CyberId == cyberId)).ToList();
            if (rates.Count > 0)
            {
                var lstRates = from r in rates
                               orderby r.UpdatedDate descending
                               select r;
                var data = lstRates.Skip((indexPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new RatingUser()
                    {
                      Id = x.Id,
                      CommentContent = x.CommentContent,
                      CreatedDate = x.CreatedDate,
                      CyberId = x.CyberId,
                      Edited = x.Edited,
                      RatePoint = x.RatePoint,
                      UpdatedDate =x.UpdatedDate,
                      UsersId = x.UsersId
                    }).ToList();
                int totalPage = rates.Count / pageSize;
                if (rates.Count % pageSize > 0)
                {
                    totalPage = (rates.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<RatingUser>>
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

        public async Task<RatingUser> GetRateUserById(int rateId)
        {
            return (await FindBy(x => x.Id == rateId)).FirstOrDefault();
        }

        public async Task<RatingUser> RateUserExist(int? cyberId, int? userId)
        {
            return (await FindBy(x => x.CyberId == cyberId & x.UsersId == userId)).FirstOrDefault();
        }
    }
}
