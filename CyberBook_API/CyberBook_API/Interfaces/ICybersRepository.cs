using CyberBook_API.Dal;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface ICybersRepository : IGenericRepository<Cyber>
    {
        Task<Cyber> GetCyberById(int? cyberId);
        Task<Cyber> GetCyberByBossCyberId(int? userId);
        Task<PagingOutput<IEnumerable<Cyber>>> GetCyberByRatePoint(int pageIndex, int pageSize);
        Task<PagingOutput<IEnumerable<Cyber>>> GetCyberByName(string cyberName, int pageIndex, int pageSize);
        Task<PagingOutput<IEnumerable<Cyber>>> GetAllCyberAvaiable(int pageIndex, int pageSize);
        Task<bool> IsBossCyber(int userId, int cyberId);
    }
}
