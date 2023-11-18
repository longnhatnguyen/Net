using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.ViewModel.PagingView;

namespace CyberBook_API.Interfaces
{
    public interface ICyberRateUserRepository : IGenericRepository<RatingUser>
    {
        Task<RatingUser> RateUserExist(int? cyberId, int? userId);
        Task<RatingUser> GetRateUserById(int rateId);
        Task<PagingOutput<IEnumerable<RatingUser>>> GetRateUserByCyberId(int cyberId, int indexPage, int pageSize);
    }
}
