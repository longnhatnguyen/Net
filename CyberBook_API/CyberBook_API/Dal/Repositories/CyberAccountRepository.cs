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
    public class CyberAccountRepository : GenericRepository<CyberAccount>, ICyberAccountRepository
    {
        public async Task<CyberAccount> CyberAccountExist(int userId, int cyberId, string cyberUsername)
        {
            return (await FindBy(x => x.UserId == userId && x.CyberId == cyberId && x.Username.Equals(cyberUsername))).FirstOrDefault();
        }

        public async Task<CyberAccount> GetCyberAccountById(int cyberAccountId)
        {
            return (await FindBy(x => x.ID == cyberAccountId)).FirstOrDefault();
        }

        public async Task<CyberAccount> GetCyberAccountDetail(int cyberAccountId, int userId)
        {
            return (await FindBy(x => x.ID == cyberAccountId && x.UserId == userId)).FirstOrDefault();
        }

        public async Task<PagingOutput<IEnumerable<CyberAccount>>> GetListCyberAccountByUserId(int userId, int index, int pageSize)
        {
            var cyberAccount = (await FindBy(x => x.UserId == userId)).ToList();
            if (cyberAccount.Count > 0)
            {
                var data = cyberAccount.Skip((index - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new CyberAccount()
                    {
                        ID = x.ID,
                        CyberId = x.CyberId,
                        CyberName = x.CyberName,
                        Password = x.Password,
                        PhoneNumber = x.PhoneNumber,
                        UserId = x.UserId,
                        Username = x.Username
                    }).ToList();

                int totalPage = cyberAccount.Count / pageSize;
                if (cyberAccount.Count % pageSize > 0)
                {
                    totalPage = (cyberAccount.Count / pageSize) + 1;
                }

                var paging = new PagingOutput<IEnumerable<CyberAccount>>
                {
                    Index = index,
                    PageSize = pageSize,
                    TotalItem = cyberAccount.Count,
                    TotalPage = totalPage,
                    Data = data
                };

                return paging;
            }
            return null;
        }
    }
}
