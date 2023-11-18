using CyberBook_API.Dal;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.PagingView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Interfaces
{
    public interface ICyberAccountRepository : IGenericRepository<CyberAccount>
    {
        Task<CyberAccount> CyberAccountExist(int userId, int cyberId, string cyberUsername);
        Task<CyberAccount> GetCyberAccountById(int cyberAccountId);
        Task<CyberAccount> GetCyberAccountDetail(int cyberAccountId, int userId);
        Task<PagingOutput<IEnumerable<CyberAccount>>> GetListCyberAccountByUserId(int userId, int index, int pageSize);
    }
}
