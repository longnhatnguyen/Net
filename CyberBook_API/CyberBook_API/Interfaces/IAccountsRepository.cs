using CyberBook_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberBook_API.Dal;


namespace CyberBook_API.Interfaces
{
    public interface IAccountsRepository : IGenericRepository<Account>
    {
        bool CheckNull(string username, string password);
        Task<Account> CheckLogin(string username, string password);
        Task<Account> GetAccountByID(int accountId);
        Task<Account> GetAccountByUsername(string username);
    }
}
