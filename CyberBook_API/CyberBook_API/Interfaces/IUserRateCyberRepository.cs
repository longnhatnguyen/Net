using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;
using CyberBook_API.ViewModel.PagingView;

namespace CyberBook_API.Interfaces
{
    public interface IUserRateCyberRepository : IGenericRepository<RatingCyber>
    {
        Task<PagingOutput<RatingCyber>> GetAllRateCyberByCyberId(int cyberId, int indexPage, int pageSize);
        Task<RatingCyber> GetRateCyberById(int rateCyberId);
        Task<RatingCyber> GetRateCyberExist(int userId, int cyberId, int orderId);
        Task<IEnumerable<RatingCyber>> GetListRateCyberByCyberId(int cyberId);
    }
}
